using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BerberKuaforRandevu.Dto.Kuafor
{
    public class KuaforOlusturDto
    {
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        [Display(Name = "Ad")]
        public required string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        [Display(Name = "Soyad")]
        public required string Soyad { get; set; }

        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Şifre en az 3 ve en fazla 100 karakter olabilir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public required string Sifre { get; set; }

        [Required]
        public required IFormFile Fotograf { get; set; }

        [Required]
        public TimeSpan MesaiBaslangic { get; set; }

        [Required]
        public TimeSpan MesaiBitis { get; set; }

        public List<int> Yetenekler { get; set; } = [];
        public List<int> Uzmanliklar { get; set; } = [];
    }

}
