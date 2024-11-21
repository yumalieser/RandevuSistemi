using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RandevuSistemi.Data;
using RandevuSistemi.Helpers;
using RandevuSistemi.Models;

namespace RandevuSistemi.Controllers
{
    public class GirisYapController : Controller
    {
        private readonly RandevuDBContext _context;
        private readonly CustomExceptionLogger _logger;

        public GirisYapController(RandevuDBContext context, CustomExceptionLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            // Kullanıcı doğrulama işlemi
            try
            {
                var kullanici = _context.Kullanicilar
                .FirstOrDefault(k => k.KullaniciMail == email && k.KullaniciSifre == password);

                if (kullanici != null)
                {
                    // Kullanıcı bilgilerini Session'a ekleme
                    HttpContext.Session.SetInt32("KullaniciID", kullanici.KullaniciID);
                    HttpContext.Session.SetInt32("KullaniciTuru", kullanici.KullaniciTuru);

                    // Kullanıcı türüne göre yönlendirme yapabilirsiniz
                    if (kullanici.KullaniciTuru == RolTip.Personel.GetHashCode())
                    {
                        if (kullanici.KullaniciOnay == false)
                        {
                            ModelState.AddModelError("", "Kullanıcı onaylanmamıştır. Lütfen yönetici onayını bekleyin.");
                            return View();
                        }
                        return RedirectToAction("Index", "Yonetim");
                    }
                    else
                    {
                        return RedirectToAction("Profil", "Kullanici");
                    }
                }
                ModelState.AddModelError("", "Geçersiz e-posta veya şifre");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return View();
            }


        }
        public IActionResult Kaydol()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Kaydol(Kullanici kullanici)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result =_context.Kullanicilar.Where(k => k.KullaniciMail == kullanici.KullaniciMail).FirstOrDefault();
                    if (result != null)
                    {
                        ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılmaktadır.");
                        return View(kullanici);
                    }
                    if (kullanici.KullaniciTuru == RolTip.User.GetHashCode())
                    {
                        kullanici.KullaniciOnay = true;
                    }
                    else
                    {
                        kullanici.KullaniciOnay = false;
                    }
                    _context.Kullanicilar.Add(kullanici);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(kullanici);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
                return View(kullanici);
            }
        }
    }
}
