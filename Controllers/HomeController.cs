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

        public async Task<IActionResult> Magaza([FromRoute] int id)
        {
            var salon = await _context.Salonlar
                .Include(salon => salon.Kuaforler)
                    .ThenInclude(kuafor => kuafor.Kullanici)
                .Include(salon => salon.Kuaforler)
                    .ThenInclude(kuafor => kuafor.KuaforYetenekler)
                    .ThenInclude(ky => ky.Yetenek)
                .Include(salon => salon.Kuaforler)
                    .ThenInclude(kuafor => kuafor.KuaforUzmanliklar)
                    .ThenInclude(ku => ku.Yetenek)
                .FirstOrDefaultAsync(salon => salon.Id == id);

            return View(salon);
        }
    }
}
