using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Dto.Yetenek
{
    public class YetenekOlusturDto
    {
        [Display]
        [Required]
        [StringLength(50)]
        public required string Ad { get; set; }

        [Display]
        [Required]
        [Range(0, 120, ErrorMessage = "Süre 0 ile 120 dakika arasında olmalıdır.")]
        public required int Sure { get; set; }

        [Display]
        [Required]
        [Range(0.0, 9999.99, ErrorMessage = "Fiyat 0 ile 9999.99 arasında olmalıdır.")]
        [DataType(DataType.Currency)]
        public required decimal Fiyat { get; set; }
    }
}
