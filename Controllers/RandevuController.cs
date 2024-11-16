using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Veritabani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Controllers
{
    [Authorize(Roles = Roller.Musteri)]
    public class RandevuController(KuaforVeritabani context) : Controller
    {
        private readonly KuaforVeritabani _context = context;

        public async Task<IActionResult> Index()
        {
            ViewBag.Salonlar = await _context.Salonlar.ToListAsync();
            return View();
        }
    }
}
