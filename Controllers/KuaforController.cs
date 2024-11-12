using BerberKuaforRandevu.Dto.Kuafor;
using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Controllers
{
    [Authorize(Roles = Roller.Admin)]
    public class KuaforController(KuaforVeritabani context, UserManager<Kullanici> userManager) : Controller
    {
        private readonly KuaforVeritabani _context = context;
        private readonly UserManager<Kullanici> _userManager = userManager;
        public async Task<IActionResult> Index()
        {
            if (User?.Identity?.Name == null) { return Unauthorized("Kullanıcı kimliği bulunamadı."); }

            string userEmail = User.Identity.Name;

            string? adminId = await GetAdminIdAsync(userEmail); 

            if (adminId == null) { return Unauthorized("Admin kullanıcı bulunamadı."); }

            Salon? salon = await GetAdminSalonAsync(adminId);

            if (salon == null)
            {
                TempData["Hata"] = "Adminin sorumlu olduğu salon bulunamadı.";
                return View("Hata");
            }

            List<Kuafor> kuaforler = await GetKuaforlerBySalonIdAsync(salon.Id);
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
            if (User?.Identity?.Name == null) { return Unauthorized("Kullanıcı kimliği bulunamadı."); }

            string userEmail = User.Identity.Name;

            string? adminId = await GetAdminIdAsync(userEmail);

            if (adminId == null) { return Unauthorized("Admin kullanıcı bulunamadı."); }

            Salon? salon = await GetAdminSalonAsync(adminId);

            if (salon == null)
            {
                TempData["Hata"] = "Adminin sorumlu olduğu salon bulunamadı.";
                return View("Hata");
            }

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

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
                SalonId = salon.Id
            };

            _context.Kuaforler.Add(kuafor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> GetAdminIdAsync(string email)
        {
            var admin = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == email);
            return admin?.Id;
        }

        private async Task<Salon?> GetAdminSalonAsync(string adminId)
        {
            return await _context.Salonlar
                .FirstOrDefaultAsync(s => s.AdminId == adminId);
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
