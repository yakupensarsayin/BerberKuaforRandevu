using BerberKuaforRandevu.Istisnalar;
using BerberKuaforRandevu.Veritabani;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Yardimci
{
    public class KuaforHelper(KuaforVeritabani context,
        IHttpContextAccessor httpContextAccessor)
    {
        private readonly KuaforVeritabani _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<int> GetSalonIdForAdminAsync()
        {
            if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User.Identity == null)
            {
                throw new AdminBulunamadiException();
            }

            string? userEmail = _httpContextAccessor.HttpContext.User.Identity.Name;

            if (userEmail == null)
            {
                throw new AdminBulunamadiException();
            }

            var admin = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == userEmail);

            if (admin == null)
            {
                throw new AdminBulunamadiException();
            }

            var salon = await _context.Salonlar
                .FirstOrDefaultAsync(s => s.AdminId == admin.Id);

            if (salon == null)
            {
                throw new AdminIcinSalonYokException();
            }

            return salon.Id;
        }
    }

}
