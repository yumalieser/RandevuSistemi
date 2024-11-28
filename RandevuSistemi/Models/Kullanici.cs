using System.ComponentModel.DataAnnotations;

namespace RandevuSistemi.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciID { get; set; }
        public string KullaniciAd { get; set; }
        public string KullaniciSoyad { get; set; }
        public string KullaniciMail { get; set; }
        public string KullaniciSifre { get; set; }
        public bool KullaniciOnay { get; set; }
        public int KullaniciTuru {  get; set; } 
    }
}
