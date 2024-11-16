namespace BerberKuaforRandevu.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public required int KuaforId { get; set; }
        public Kuafor Kuafor { get; set; } = null!;
        public required int YetenekId { get; set; }
        public Yetenek Yetenek { get; set; } = null!;
        public required string KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; } = null!;
        public required DateTime BaslangicTarihi { get; set; }
        public required DateTime BitisTarihi { get; set; }
        public bool Onayli { get; set; } = false;
    }

}
