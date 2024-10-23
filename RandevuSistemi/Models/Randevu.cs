using System.ComponentModel.DataAnnotations;

namespace RandevuSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }
        public int RandevuTuru { get; set; }
        public DateTime RandevuZamani { get; set; }
        public bool RandevuOnay { get; set; }
    }
}
