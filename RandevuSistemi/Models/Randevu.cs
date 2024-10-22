namespace RandevuSistemi.Models
{
    public class Randevu
    {
        public int RandevuID { get; set; }
        public int RandevuTuru { get; set; }
        public DateTime RandevuZamani { get; set; }
        public bool RandevuOnay { get; set; }
    }
}
