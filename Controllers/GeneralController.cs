using BerberKuaforRandevu.Dto.Randevu;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BerberKuaforRandevu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController(KuaforVeritabani context) : Controller
    {
        private readonly KuaforVeritabani _context = context;

        [HttpGet("salonlar")]
        public async Task<IActionResult> GetSalonlar()
        {
            var salonlar = await _context.Salonlar
                .Select(s => new
                {
                    s.Id,
                    s.Ad
                })
                .ToListAsync();

            return Ok(salonlar);
        }

        [HttpGet("yetenekler")]
        public async Task<IActionResult> GetYetenekler([FromQuery] int salonId)
        {
            var yetenekler = await _context.Yetenekler
                .Where(y => y.SalonId == salonId)
                .Select(y => new
                {
                    y.Id,
                    y.Ad,
                    y.Fiyat,
                    y.Sure
                })
                .ToListAsync();

            if (yetenekler == null)
            {
                return NotFound();
            }

            return Ok(yetenekler);
        }

        [HttpGet("kuaforler")]
        public async Task<IActionResult> GetKuaforler([FromQuery] int salonId, [FromQuery] int yetenekId)
        {
            var kuaforler = await _context.Kuaforler
                .Include(k => k.KuaforYetenekler)
                    .ThenInclude(ky => ky.Yetenek)
                .Include(k => k.KuaforUzmanliklar)
                    .ThenInclude(ky => ky.Yetenek)
                 .Include(k => k.Kullanici)
                .Where(k => k.SalonId == salonId && k.KuaforYetenekler.Any(ky => ky.YetenekId == yetenekId))
                .Select(k => new
                {
                    k.Id,
                    k.Kullanici.Ad,
                    k.Kullanici.Soyad,
                    Uzmanlik = k.KuaforUzmanliklar.Any(ku => ku.YetenekId == yetenekId) ? "yes" : "no",
                    k.MesaiBaslangic,
                    k.MesaiBitis
                })
                .ToListAsync();

            return Ok(kuaforler);
        }

        [HttpGet("kuaforler/randevular")]
        public async Task<IActionResult> GetKuaforRandevular([FromQuery] int kuaforId, [FromQuery] DateTime date)
        {
            var startDate = date.Date; // Seçilen günün başlangıcı (00:00:00)
            var endDate = startDate.AddDays(1); // Seçilen günün sonu (23:59:59)

            var randevular = await _context.Randevular
                .Where(r => r.KuaforId == kuaforId &&
                            r.BaslangicTarihi >= startDate && r.BaslangicTarihi < endDate)
                .Select(r => new
                {
                    r.Id,
                    title = r.Yetenek.Ad,
                    start = r.BaslangicTarihi,
                    end = r.BitisTarihi,
                    r.Onayli
                })
                .ToListAsync();

            return Ok(randevular);
        }


        [HttpGet("kuaforler/mesai")]
        public async Task<IActionResult> GetKuaforMesai([FromQuery] int kuaforId)
        {
            var kuafor = await _context.Kuaforler
                .Where(k => k.Id == kuaforId)
                .Select(k => new
                {
                    k.MesaiBaslangic,
                    k.MesaiBitis
                })
                .FirstOrDefaultAsync();

            if (kuafor == null)
            {
                return NotFound();
            }

            return Ok(kuafor);
        }

        [HttpPost("randevuAl")]
        public async Task<IActionResult> RandevuAl([FromBody] RandevuAlmaModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kuafor = await _context.Kuaforler.FindAsync(model.KuaforId);
            if (kuafor == null)
            {
                return NotFound("Kuaför bulunamadı.");
            }

            var yetenek = await _context.Yetenekler.FindAsync(model.YetenekId);
            if (yetenek == null)
            {
                return NotFound("Yetenek bulunamadı.");
            }

            var kullaniciEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(kullaniciEmail))
            {
                return Unauthorized("Kullanıcı doğrulaması başarısız.");
            }

            var kullanici = await _context.Kullanicilar.SingleOrDefaultAsync(u => u.Email == kullaniciEmail);
            if (kullanici == null)
            {
                return Unauthorized("Kullanıcı bulunamadı.");
            }

            var baslangicTarihi = model.BaslangicTarihi;
            baslangicTarihi = baslangicTarihi.AddHours(3);
            var bitisTarihi = baslangicTarihi.AddMinutes(yetenek.Sure);

            var mevcutRandevular = await _context.Randevular
                .Where(r => r.KuaforId == model.KuaforId &&
                            ((r.BaslangicTarihi < bitisTarihi && r.BitisTarihi > baslangicTarihi)))
                .ToListAsync();

            if (mevcutRandevular.Any())
            {
                return Conflict("Seçilen zaman diliminde mevcut bir randevu var.");
            }

            var randevu = new Randevu
            {
                KuaforId = model.KuaforId,
                YetenekId = model.YetenekId,
                BaslangicTarihi = baslangicTarihi,
                BitisTarihi = bitisTarihi,
                Onayli = false,
                KullaniciId = kullanici.Id
            };

            _context.Randevular.Add(randevu);
            await _context.SaveChangesAsync();

            return Json(new { Message = "Randevu başarıyla alındı.", RandevuId = randevu.Id });
        }

    }
}
