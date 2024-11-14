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

        [Required]
        public required byte[] Fotograf { get; set; }
        [Required]
        public TimeSpan MesaiBaslangic { get; set; }
        [Required]
        public TimeSpan MesaiBitis { get; set; }

        public List<KuaforYetenek> KuaforYetenekler { get; set; } = [];
        public List<KuaforUzmanlik> KuaforUzmanliklar { get; set; } = [];
    }
}
