using Microsoft.AspNetCore.Identity;

namespace BerberKuaforRandevu.Models
{
    public class Kullanici : IdentityUser
    {
        public int Ad { get; set; }
        public int Soyad { get; set; }
    }
}
