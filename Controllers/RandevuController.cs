using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Repository;
using BerberKuaforRandevu.Veritabani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Controllers
{
    public class RandevuController(KuaforVeritabani context, RandevuRepository randevuRepository) : Controller
    {
        private readonly KuaforVeritabani _context = context;
        private readonly RandevuRepository _randevuRepository = randevuRepository;

        [Authorize(Roles = Roller.Musteri)]
        public async Task<IActionResult> Index()
        {
            ViewBag.Salonlar = await _context.Salonlar.ToListAsync();
            return View();
        }

        [Authorize(Roles = Roller.Musteri)]
        public async Task<IActionResult> Randevularim()
        {
            string email = User.Identity!.Name!;

            var kullanici = await _context.Kullanicilar
                .Where(k => k.Email == email)
                .FirstAsync()!;

            var randevularim = await _randevuRepository.GetRandevuAsync(q =>
                q.Where(r => r.KullaniciId == kullanici.Id)
                 .Include(r => r.Kuafor)
                    .ThenInclude(k => k.Salon)
                 .Include(r => r.Kuafor)
                    .ThenInclude(k => k.Kullanici)
                 .Include(r => r.Yetenek)
                 .OrderBy(r => r.BaslangicTarihi)
            );

            return View(randevularim);
        }

        [Authorize(Roles = Roller.Kuafor)]
        public async Task<IActionResult> RandevulariYonet()
        {
            Kuafor kuafor = await GetCurrentKuafor();

            var randevular = await _randevuRepository.GetRandevuAsync(q => 
                q.Where(r => r.KuaforId == kuafor.Id && r.Onayli == false)
                 .Include(r => r.Kullanici)
            );

            return View(kuafor);
        }

        [Authorize(Roles = Roller.Kuafor)]
        [HttpPost("RandevuOnayla/{id}")]
        public async Task<IActionResult> RandevuOnayla([FromRoute] int id)
        {
            Kuafor kuafor = await GetCurrentKuafor();

            Randevu? randevu = await _context.Randevular
                .Where(r => r.KuaforId == kuafor.Id && r.Onayli == false)
                .FirstOrDefaultAsync();

            if (randevu == null)
            {
                return BadRequest();
            }

            randevu.Onayli = true;
            _context.Update(randevu);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = Roller.Kuafor)]
        public async Task<IActionResult> KuaforOlarakRandevularim()
        {
            Kuafor kuafor = await GetCurrentKuafor();

            List<Randevu>? randevu = await _context.Randevular
                .Where(r => r.KuaforId == kuafor.Id && r.Onayli == true && r.BaslangicTarihi >= DateTime.Now)
                .Include(r => r.Kullanici)
                .Include(r => r.Yetenek)
                .OrderBy(r => r.BaslangicTarihi)
                .ToListAsync();

            return View(randevu);
        }

        private async Task<Kuafor> GetCurrentKuafor()
        {
            string email = User.Identity!.Name!;

            var kullanici = await _context.Kullanicilar
                .Where(k => k.Email == email)
                .FirstAsync()!;

            var kuafor = await _context.Kuaforler
                .Where(k => k.KullaniciId == kullanici.Id)
                .FirstAsync();

            return kuafor;
        }

    }
}
