using BerberKuaforRandevu.Dto.Kuafor;
using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using BerberKuaforRandevu.Yardimci;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class KuaforController(KuaforVeritabani context, UserManager<Kullanici> userManager,
            KuaforHelper helper) : Controller
    {
        private readonly KuaforVeritabani _context = context;
        private readonly UserManager<Kullanici> _userManager = userManager;
        private readonly KuaforHelper _helper = helper;

        public async Task<IActionResult> Index()
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();
            List<Kuafor> kuaforler = await GetKuaforlerBySalonIdAsync(salonId);
            return View(kuaforler);
        }

        [HttpGet]
        public IActionResult Olustur()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Olustur(KuaforOlusturDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            int salonId = await _helper.GetSalonIdForAdminAsync();

            var kullanici = new Kullanici
            {
                Ad = dto.Ad,
                Soyad = dto.Soyad,
                Email = dto.Email,
                UserName = dto.Email
            };

            var sonuc = await _userManager.CreateAsync(kullanici, dto.Sifre);

            if (!sonuc.Succeeded)
            {
                foreach (var error in sonuc.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(dto);
            }

            // Kuaförün rolünü atamayı unutmayalım
            await _userManager.AddToRoleAsync(kullanici, Roller.Kuafor);

            var kuafor = new Kuafor
            {
                KullaniciId = kullanici.Id,
                SalonId = salonId
            };

            _context.Kuaforler.Add(kuafor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
 
        private async Task<List<Kuafor>> GetKuaforlerBySalonIdAsync(int salonId)
        {
            return await _context.Kuaforler
                .Where(k => k.SalonId == salonId)
                .Include(k => k.Kullanici)
                .Include(k => k.KuaforYetenekler)
                    .ThenInclude(ky => ky.Yetenek)
                .Include(k => k.KuaforUzmanliklar)
                    .ThenInclude(ku => ku.Yetenek)
                .ToListAsync();
        }
    }
}
