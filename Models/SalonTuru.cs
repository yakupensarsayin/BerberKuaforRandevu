using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Models
{
    public class SalonTuru
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; } = string.Empty;

        public List<Salon> Salonlar { get; set; } = [];
    }

}
