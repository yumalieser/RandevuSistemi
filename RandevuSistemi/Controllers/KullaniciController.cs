using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Data;
using RandevuSistemi.Helpers;
using RandevuSistemi.Models;

namespace RandevuSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly RandevuDBContext _context;
        private readonly CustomExceptionLogger _logger;

        public KullaniciController(RandevuDBContext context, CustomExceptionLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Profil()
        {
            // Kullanıcı bilgilerini Session'dan alınması
            int kullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            var kullanici = _context.Kullanicilar.Find(kullaniciID);
            return View(kullanici);
        }
        public IActionResult CikisYap()
        {
            // Session temizleme
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "GirisYap");
        }

        public IActionResult RandevuAl()
        {
            // Randevu alınacak doktorların listelenmesi
            var randevular = _context.Randevular
            .Where(k => k.BitisTarihi > DateTime.Now)
            .ToList();
            foreach (var randevu in randevular)
            {
                randevu.AlinanRandevular = _context.AlinanRandevular.Where(a => a.RandevuID == randevu.RandevuID).ToList();
            }
            //alınan randevu saatlerinde tekrar randevu alınmaması için
            return View(randevular);
        }

        [HttpPost]
        public IActionResult Randevularim()
        {
            // Kullanıcının randevularını getirme
            int kullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            var randevular = _context.AlinanRandevular
            .Where(r => r.KullaniciID == kullaniciID)
            .ToList();
            return View(randevular);
        }

        [HttpPost]
        public IActionResult RandevuAl(AlinanRandevu alinanRandevu)
        {
            // Randevu alınması
            int kullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            alinanRandevu.KullaniciID = kullaniciID;
            _context.AlinanRandevular.Add(alinanRandevu);
            _context.SaveChanges();
            return RedirectToAction("Randevularim");
        }
    }
}