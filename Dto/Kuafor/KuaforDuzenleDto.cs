using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Dto.Kuafor
{
    public class KuaforDuzenleDto
    {
        public int Id { get; set; }

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

        public string? Sifre { get; set; }

        public IFormFile? Fotograf { get; set; }

        [Required]
        public TimeSpan MesaiBaslangic { get; set; }

        [Required]
        public TimeSpan MesaiBitis { get; set; }

        public List<int> Yetenekler { get; set; } = [];
        public List<int> Uzmanliklar { get; set; } = [];
    }
}
