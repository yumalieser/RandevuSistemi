using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Data;
using RandevuSistemi.Models;

namespace RandevuSistemi.Controllers
{
    public class GirisYapController : Controller
    {
        private readonly RandevuDBContext _context;

        public GirisYapController(RandevuDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            // Kullanıcı doğrulama işlemi
            var kullanici = _context.Kullanicilar
                .FirstOrDefault(k => k.KullaniciMail == email && k.KullaniciSifre == password);

            if (kullanici != null)
            {
                // Kullanıcı bilgilerini Session'a ekleme
                HttpContext.Session.SetInt32("KullaniciID", kullanici.KullaniciID);
                HttpContext.Session.SetInt32("KullaniciTuru", kullanici.KullaniciTuru);

                // Kullanıcı türüne göre yönlendirme yapabilirsiniz
                if (kullanici.KullaniciTuru == 1)
                {
                    return RedirectToAction("Index", "Yonetim");
                }
                else
                {
                    return RedirectToAction("UserDashboard", "User");
                }
            }

            ModelState.AddModelError("", "Geçersiz e-posta veya şifre");
            return View();
        }
        public IActionResult Kaydol()
        {
            return View();
        }
    }
}
