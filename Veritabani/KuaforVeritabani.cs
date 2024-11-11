using BerberKuaforRandevu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Veritabani
{
    public class KuaforVeritabani(DbContextOptions<KuaforVeritabani> options) : IdentityDbContext<Kullanici>(options)
    {
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kuafor> Kuaforler { get; set; }
        public DbSet<Yetenek> Yetenekler { get; set; }
        public DbSet<KuaforYetenek> KuaforlerYetenekler { get; set; }
        public DbSet<KuaforUzmanlik> KuaforlerUzmanliklar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            base.OnModelCreating(modelBuilder); 
            
            /*
             * Normalde bunlar ara tablolar ancak EF her tablo icin ID istiyor.
             * Onu susturmak için, o tabloların da Id'leri bu olsun dedik.
             */

            modelBuilder.Entity<KuaforUzmanlik>()
                .HasKey(ku => new { ku.KuaforId, ku.YetenekId }); 
            
            modelBuilder.Entity<KuaforYetenek>()
                .HasKey(ky => new { ky.KuaforId, ky.YetenekId }); }
    }
}
