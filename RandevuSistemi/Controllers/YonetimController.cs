using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Filters;
using RandevuSistemi.Models;
using RandevuSistemi.Data;

namespace RandevuSistemi.Controllers
{
    [AdminAuthorize]
    public class YonetimController : Controller
    {

        public YonetimController(RandevuDBContext context)
        {
            _context = context;
        }

        private readonly RandevuDBContext _context;

        public IActionResult Index()
        {
            return View();
        }

        /* RANDEVU İŞLEMLERİ */
        public IActionResult RandevuListele()
        {
            var randevular = _context.Randevular.ToList();
            return View(randevular);
        }
        public IActionResult AktifRandevuListele()
        {
             // RandevuOnay durumu true olan randevuları alıyoruz
            var onayliRandevular = _context.Randevular
                                           .Where(r => r.RandevuOnay == true)
                                           .ToList();

            return View(onayliRandevular);
        }
        [HttpGet]
        public IActionResult RandevuEkle()
        {
            // Enum değerlerini ViewBag'e aktarıyoruz
            ViewBag.RandevuTurleri = Enum.GetValues(typeof(RandevuTuru))
                                        .Cast<RandevuTuru>()
                                        .Select(r => new { Id = (int)r, Name = r.ToString() })
                                        .ToList();
            return View();
        }
        [HttpPost]
        public IActionResult RandevuEkle(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                // Randevu kaydını veritabanına ekliyoruz
                randevu.RandevuOnay = false; // Başlangıçta onaylanmamış olarak ayarlayabilirsiniz
                _context.Randevular.Add(randevu);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Başka bir sayfaya yönlendirin veya onay mesajı verin
            }

            // Post işleminde bir hata olursa, ViewBag tekrar atanmalı
            ViewBag.RandevuTurleri = Enum.GetValues(typeof(RandevuTuru))
                .Cast<RandevuTuru>()
                .Select(r => new { Id = (int)r, Name = r.ToString() })
                .ToList();

            return View(randevu);
        }

        public IActionResult RandevuOnay()
        {
            var randevular = _context.Randevular.ToList();
            return View(randevular);
        }
        public IActionResult RandevuSil()
        {
            var randevular = _context.Randevular.ToList();
            return View(randevular);
        }

        /* KULLANICI İŞLEMLERİ */
        public IActionResult KullaniciListele()
        {
            var kullanicilar = _context.Kullanicilar.ToList();
            return View(kullanicilar);
        }
        public IActionResult OnayliKullaniciListele()
        {
            return View();
        }
        public IActionResult KullaniciSil()
        {
            var kullanicilar = _context.Kullanicilar.ToList();
            return View(kullanicilar);
        }

        [HttpGet]
        public IActionResult KullaniciEkle()
        {
            return View();
        }

        // Kullanıcı Ekleme işlemi
        [HttpPost]
        public IActionResult KullaniciEkle(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı veritabanına ekle
                _context.Kullanicilar.Add(kullanici);
                _context.SaveChanges(); // Değişiklikleri kaydet

                // Kullanıcı başarıyla eklendikten sonra yönlendirme
                return RedirectToAction("KullaniciListele", "Yonetim"); // Veya başka bir sayfaya yönlendirin
            }

            // Model geçersiz ise, kullanıcı bilgileriyle birlikte view'i geri döndür
            return View(kullanici);
        }
    }
}
