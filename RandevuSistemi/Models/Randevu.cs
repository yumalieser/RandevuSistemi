using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandevuSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }
        [Column(TypeName = "int")]
        public RandevuTuru RandevuTuru { get; set; }  // Enum olarak güncellendi        
        public DateTime RandevuZamani { get; set; }
        public bool RandevuOnay { get; set; }
    }
    public enum RandevuTuru
    {
    DoktorRandevusu = 1,
    DisciRandevusu = 2,
    GozRandevusu = 3,
    GenelKontrol = 4
    }
}
