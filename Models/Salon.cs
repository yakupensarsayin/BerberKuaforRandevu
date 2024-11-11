using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Models
{
    public class Salon
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ad { get; set; } = string.Empty;

        [Required]
        public required string AdminId { get; set; }
        public Kullanici Admin { get; set; } = null!;

        [Required]
        public required int SalonTuruId { get; set; }
        public SalonTuru SalonTuru { get; set; } = null!;

        public List<Kuafor> Kuaforler { get; set; } = [];
    }

}
