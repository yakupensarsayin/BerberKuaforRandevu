using BerberKuaforRandevu.Dto.Yetenek;
using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using BerberKuaforRandevu.Yardimci;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace BerberKuaforRandevu.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class YetenekController(KuaforVeritabani context, KuaforHelper helper) : Controller
    {
        private readonly KuaforVeritabani _context = context;
        private readonly KuaforHelper _helper = helper;
        public async Task<IActionResult> Index()
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();

            var yetenekler = await _context.Yetenekler
                .Where(y => y.SalonId == salonId)
                .ToListAsync();

            return View(yetenekler);
        }

        [HttpGet]
        public IActionResult Olustur()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Duzenle([FromRoute] int id)
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();

            Yetenek? yetenek = await _context.Yetenekler
                .Where(y => y.SalonId == salonId && y.Id == id)
                .FirstOrDefaultAsync();

            if (yetenek == null)
            {
                TempData["Hata"] = "Salonunuz için böyle bir yetenek yok.";
                return View("Hata");
            }

            var yetenekDuzenleDto = new YetenekDuzenleDto
            {
                Id = yetenek.Id,
                Ad = yetenek.Ad,
                Sure = yetenek.Sure,
                Fiyat = yetenek.Fiyat,
            };

            return View(yetenekDuzenleDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Olustur(YetenekOlusturDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            int salonId = await _helper.GetSalonIdForAdminAsync();

            var yetenek = new Yetenek
            {
                SalonId = salonId,
                Ad = dto.Ad,
                Sure = dto.Sure,
                Fiyat = dto.Fiyat,
            };

            _context.Yetenekler.Add(yetenek);
            await _context.SaveChangesAsync();
            TempData["Mesaj"] = "Yetenek başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle([FromRoute] int id, YetenekDuzenleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (dto.Id != id)
            {
                TempData["Hata"] = "Düzenlenmek istenen yeteneğin ID'si eşleşmiyor."; 
                return View("Hata");
            }

            int salonId = await _helper.GetSalonIdForAdminAsync();

            Yetenek? guncellenecekYetenek = await _context.Yetenekler
                .Where(y => y.SalonId == salonId && y.Id == id)
                .FirstOrDefaultAsync();

            if (guncellenecekYetenek == null)
            {
                TempData["Hata"] = "Düzenlenmek istenen yetenek bulunamadı veya bu yetenek size ait değil.";
                return View("Hata");
            }

            guncellenecekYetenek.Ad = dto.Ad; 
            guncellenecekYetenek.Sure = dto.Sure; 
            guncellenecekYetenek.Fiyat = dto.Fiyat;

            _context.Update(guncellenecekYetenek);
            await _context.SaveChangesAsync();
            TempData["Mesaj"] = "Yetenek başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil([FromRoute] int id)
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();

            Yetenek? yetenek = await _context.Yetenekler
                .Where(y => y.SalonId == salonId && y.Id == id)
                .FirstOrDefaultAsync();

            if (yetenek == null)
            {
                TempData["Hata"] = "Silinmek istenen yetenek bulunamadı veya bu yetenek size ait değil.";
                return RedirectToAction(nameof(Index));
            }

            _context.Yetenekler.Remove(yetenek);
            await _context.SaveChangesAsync();

            TempData["Mesaj"] = "Yetenek başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
