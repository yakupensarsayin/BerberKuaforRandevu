using System.ComponentModel.DataAnnotations;

namespace BerberKuaforRandevu.Models
{
    public class Yetenek
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; } = string.Empty;

        public List<KuaforYetenek> KuaforYetenekler { get; set; } = [];
        public List<KuaforUzmanlik> KuaforUzmanliklar { get; set; } = [];
    }
}
