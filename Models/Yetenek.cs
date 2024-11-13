using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Models
{
    public class Yetenek
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; } = string.Empty;

        [Required]
        [Range(0, 120, ErrorMessage = "Süre 0 ile 120 dakika arasında olmalıdır.")] 
        public int Sure { get; set; } 

        [Required]
        [Range(0.0, 999999.99, ErrorMessage = "Fiyat 0 ile 9999.99 arasında olmalıdır.")] 
        [DataType(DataType.Currency)] 
        public decimal Fiyat { get; set; } 

        [Required]
        public int SalonId { get; set; }
        public Salon Salon { get; set; } = null!;

        public List<KuaforYetenek> KuaforYetenekler { get; set; } = [];
        public List<KuaforUzmanlik> KuaforUzmanliklar { get; set; } = [];
    }
}
