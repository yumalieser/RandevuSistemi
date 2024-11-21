using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandevuSistemi.Models
{
    public class AlinanRandevu
    {
        [Key]
        public int AlinanRandevuID { get; set; }
        public int RandevuID { get; set; }
        public int KullaniciID { get; set; }
        public bool RandevuOnay { get; set; }
        public bool RandevuIptal { get; set; }
        public string RandevuAciklama { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public TimeSpan RandevuSaati { get; set; }
    }
}