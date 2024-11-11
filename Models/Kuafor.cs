using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Models
{
    public class Kuafor
    {
        public int Id { get; set; }

        [Required]
        public required string KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; } = null!;

        [Required]
        public int SalonId { get; set; }
        public Salon Salon { get; set; } = null!;

        public List<KuaforYetenek> KuaforYetenekler { get; set; } = [];
        public List<KuaforUzmanlik> KuaforUzmanliklar { get; set; } = [];
    }
}
