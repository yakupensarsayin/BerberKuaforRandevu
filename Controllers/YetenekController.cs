using BerberKuaforRandevu.Dto.Yetenek;
using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using BerberKuaforRandevu.Yardimci;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return RedirectToAction(nameof(Index));
        }
    }
}
