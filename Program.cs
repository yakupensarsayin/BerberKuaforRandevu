using BerberKuaforRandevu.Genel;
using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KuaforVeritabani>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDefaultIdentity<Kullanici>(o => 
{
    o.Password.RequireDigit = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.Password.RequiredUniqueChars = 1;
    o.Password.RequiredLength = 3;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<KuaforVeritabani>()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

#region Varsayilan rolleri ve admin kullanicilarini olusturalim.
using (var scope = app.Services.CreateScope())
{
    var rolYoneticisi = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var kullaniciYoneticisi = scope.ServiceProvider.GetRequiredService<UserManager<Kullanici>>();

    string[] roller = [Roller.Admin, Roller.Kuafor, Roller.Musteri];
    foreach (string rol in roller)
    {
        if (!await rolYoneticisi.RoleExistsAsync(rol))
        {
            await rolYoneticisi.CreateAsync(new IdentityRole(rol));
        }
    }

    string[] emailler = ["yakup.sayin@ogr.sakarya.edu.tr", "sude.kocaacar@ogr.sakarya.edu.tr"];
    foreach (string email in emailler)
    {
        if (await kullaniciYoneticisi.FindByEmailAsync(email) == null)
        {
            string ad = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(email.Split('@')[0].Split('.')[0]);
            string soyad = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(email.Split('@')[0].Split('.')[1]);

            var kullanici = new Kullanici
            {
                Email = email,
                UserName = email,
                Ad = ad,
                Soyad = soyad
            };

            string sifre = "sau";
            await kullaniciYoneticisi.CreateAsync(kullanici, sifre);
            await kullaniciYoneticisi.AddToRoleAsync(kullanici, Roller.Admin);
        }
    }
}
#endregion

app.Run();
