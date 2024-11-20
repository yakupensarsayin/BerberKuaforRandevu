using BerberKuaforRandevu.Veritabani;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Controllers
{
    public class HomeController(KuaforVeritabani context) : Controller
    {
        private readonly KuaforVeritabani _context = context;
        public async Task<IActionResult> Index()
        {
            ViewBag.Salonlar = await _context.Salonlar
                .ToListAsync();

            return View();
        }
    }
}
