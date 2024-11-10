using Microsoft.AspNetCore.Mvc;

namespace BerberKuaforRandevu.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
