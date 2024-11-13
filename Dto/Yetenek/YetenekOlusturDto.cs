using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Dto.Yetenek
{
    public class YetenekOlusturDto
    {
        [Required]
        [StringLength(50)]
        public required string Ad { get; set; }
    }
}
