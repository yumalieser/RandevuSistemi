using System.ComponentModel.DataAnnotations;

namespace RandevuSistemi.Models
{
    public class RandevuTur
    {
        [Key]
        public int RandevuTurID { get; set; }
        public string RandevuTurAdi { get; set; }
    }
}