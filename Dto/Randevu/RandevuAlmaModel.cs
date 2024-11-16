using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Dto.Randevu
{
    public class RandevuAlmaModel
    {
        [Required]
        public int KuaforId { get; set; }

        [Required]
        public int YetenekId { get; set; }

        [Required]
        public DateTime BaslangicTarihi { get; set; }

        [Required]
        public decimal Ucret { get; set; }
    }

}