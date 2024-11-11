namespace BerberKuaforRandevu.Models
{
    public class KuaforYetenek
    {
        public int KuaforId { get; set; }
        public Kuafor Kuafor { get; set; } = null!;

        public int YetenekId { get; set; }
        public Yetenek Yetenek { get; set; } = null!;
    }
}
