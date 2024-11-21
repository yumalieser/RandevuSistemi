using System.ComponentModel.DataAnnotations;

namespace RandevuSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }
        public int CreatedByKullanici { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
        public int RandevuSuresi { get; set; } // Duration in minutes
        public int RandevuTur { get; set; }  // Enum olarak güncellendi        
        public List<AlinanRandevu> Randevular { get; set; }
        public bool RandevuOnay { get; set; }
    }
}
