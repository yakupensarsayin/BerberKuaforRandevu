using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Veritabani;
using BerberKuaforRandevu.Yardimci;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Controllers
{
    public class RaporController(KuaforHelper helper, KuaforVeritabani context) : Controller
    {
        private readonly KuaforHelper _helper = helper;
        private readonly KuaforVeritabani _context = context;

        [Authorize(Roles = Roller.Admin)]
        public async Task<IActionResult> Index()
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();
            var kuaforler = await _context.Kuaforler
                .Where(k => k.SalonId == salonId)
                .Include(k => k.Kullanici)
                .ToListAsync();

            return View(kuaforler);
        }

        [HttpGet("KuaforYetenekRaporu")]
        [Authorize(Roles = Roller.Admin)]
        public async Task<IActionResult> GetKuaforYetenekRaporu([FromQuery] int kuaforId)
        {
            var randevular = await _context.Randevular
                .Where(r => r.KuaforId == kuaforId)
                .GroupBy(r => r.Yetenek.Ad)
                .Select(g => new { Yetenek = g.Key, count = g.Count() })
                .ToListAsync();

            return Ok(randevular);
        }

    }
}
