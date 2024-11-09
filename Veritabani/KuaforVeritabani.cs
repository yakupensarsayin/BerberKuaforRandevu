using BerberKuaforRandevu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Veritabani
{
    public class KuaforVeritabani(DbContextOptions<KuaforVeritabani> options) : IdentityDbContext<Kullanici>(options)
    {
        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
