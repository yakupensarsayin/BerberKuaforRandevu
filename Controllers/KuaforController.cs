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
        public async Task<IActionResult> Olustur()
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();

            ViewBag.Yetenekler = await _context.Yetenekler
                .Where(y => y.SalonId == salonId)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Olustur([FromBody] KuaforOlusturDto dto)
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

            // Yetenekler ve Uzmanlıklar kontrolü ve ekleme
            var validationResponse = await ValidateAndAssignYeteneklerUzmanliklarAsync(kuafor, dto.Yetenekler,
                dto.Uzmanliklar, salonId);

            if (validationResponse != null)
            {
                return validationResponse;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Duzenle([FromRoute] int id)
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();
            var kuaforler = await GetKuaforlerBySalonIdAsync(salonId);

            var kuafor = kuaforler.Find(k => k.Id == id);

            if (kuafor == null)
            {
                TempData["Hata"] = "Böyle bir kuaför yok ya da salonunuza ait değil.";
                return View("Hata");
            }

            ViewBag.Yetenekler = await _context.Yetenekler
                .Where(y => y.SalonId == salonId)
                .ToListAsync();

            var model = new KuaforDuzenleDto
            {
                Id = kuafor.Id,
                Ad = kuafor.Kullanici.Ad,
                Soyad = kuafor.Kullanici.Soyad,
                Email = kuafor.Kullanici.Email!,
                Yetenekler = kuafor.KuaforYetenekler.Select(ky => ky.YetenekId).ToList(),
                Uzmanliklar = kuafor.KuaforUzmanliklar.Select(ku => ku.YetenekId).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle([FromRoute] int id, [FromBody] KuaforDuzenleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            int salonId = await _helper.GetSalonIdForAdminAsync();
            var kuaforler = await GetKuaforlerBySalonIdAsync(salonId);

            var kuafor = kuaforler.Find(k => k.Id == id);

            if (kuafor == null)
            {
                TempData["Hata"] = "Böyle bir kuaför yok ya da salonunuza ait değil.";
                return View("Hata");
            }

            // Kullanıcı bilgilerini güncelle
            var updateResponse = await UpdateKullaniciAsync(kuafor.Kullanici, dto);
            if (updateResponse != null)
            {
                return updateResponse;
            }

            // Yetenekler ve Uzmanlıklar kontrolü ve ekleme
            var validationResponse = await ValidateAndAssignYeteneklerUzmanliklarAsync(kuafor, dto.Yetenekler,
                dto.Uzmanliklar, salonId);

            if (validationResponse != null)
            {
                return validationResponse;
            }

            await _context.SaveChangesAsync();
            TempData["Mesaj"] = "Kuaför başarıyla güncellendi!";
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil([FromRoute] int id)
        {
            int salonId = await _helper.GetSalonIdForAdminAsync();
            var kuaforler = await GetKuaforlerBySalonIdAsync(salonId);
            var kuafor = kuaforler.Find(k => k.Id == id);
            if (kuafor == null)
            {
                TempData["Hata"] = "Böyle bir kuaför yok ya da salonunuza ait değil.";
                return RedirectToAction(nameof(Index));
            }
            _context.Kullanicilar.Remove(kuafor.Kullanici);
            _context.Kuaforler.Remove(kuafor);
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

        private async Task<IActionResult?> UpdateKullaniciAsync(Kullanici kullanici, KuaforDuzenleDto dto)
        {
            kullanici.Ad = dto.Ad;
            kullanici.Soyad = dto.Soyad;
            kullanici.Email = dto.Email;
            kullanici.UserName = dto.Email;

            // Şifre boş değilse, şifreyi güncelle
            if (!string.IsNullOrEmpty(dto.Sifre))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(kullanici);
                var sonuc = await _userManager.ResetPasswordAsync(kullanici, token, dto.Sifre);

                if (!sonuc.Succeeded)
                {
                    foreach (var error in sonuc.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return null;
        }

        private async Task<IActionResult?> ValidateAndAssignYeteneklerUzmanliklarAsync(Kuafor kuafor, 
            List<int> yetenekler, List<int> uzmanliklar, int salonId)
        {
            var salonYetenekler = await _context.Yetenekler
                .Where(y => y.SalonId == salonId).Select(y => y.Id).ToListAsync();

            // Mevcut yetenekleri temizle ve güncelle
            kuafor.KuaforYetenekler.Clear();
            foreach (var yetenekId in yetenekler)
            {
                if (!salonYetenekler.Contains(yetenekId))
                {
                    return BadRequest($"Yetenek ID {yetenekId} bu salona ait değil.");
                }
                var kuaforYetenek = new KuaforYetenek
                {
                    KuaforId = kuafor.Id,
                    YetenekId = yetenekId
                };
                kuafor.KuaforYetenekler.Add(kuaforYetenek);
            }

            // Mevcut uzmanlıkları temizle ve güncelle
            kuafor.KuaforUzmanliklar.Clear();
            foreach (var uzmanlikId in uzmanliklar)
            {
                if (!salonYetenekler.Contains(uzmanlikId))
                {
                    return BadRequest($"Uzmanlık ID {uzmanlikId} bu salona ait değil.");
                }
                var kuaforUzmanlik = new KuaforUzmanlik
                {
                    KuaforId = kuafor.Id,
                    YetenekId = uzmanlikId
                };
                kuafor.KuaforUzmanliklar.Add(kuaforUzmanlik);
            }

            return null;
        }
    }
}
