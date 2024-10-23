using Microsoft.AspNetCore.Mvc;

namespace RandevuSistemi.Controllers
{
    public class YonetimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /* RANDEVU İŞLEMLERİ */
        public IActionResult RandevuListele()
        {
            return View();
        }
        public IActionResult AktifRandevuListele()
        {
            return View();
        }
        public IActionResult RandevuEkle()
        {
            return View();
        }
        public IActionResult RandevuOnay()
        {
            return View();
        }

        /* KULLANICI İŞLEMLERİ */
        public IActionResult KullaniciListele()
        {
            return View();
        }
        public IActionResult KullaniciOnay()
        {
            return View();
        }
    }
}
