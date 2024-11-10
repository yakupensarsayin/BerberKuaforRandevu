using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Models
{
    public class Kullanici : IdentityUser
    {
        [StringLength(50)]
        [Required]
        public required string Ad { get; set; }
        [Required]
        [StringLength(50)]
        public required string Soyad { get; set; }
    }
}
