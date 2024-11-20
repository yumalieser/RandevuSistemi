using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RandevuSistemi.Filters
{
    public class AdminAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var kullaniciTuru = context.HttpContext.Session.GetInt32("KullaniciTuru");

            // Eğer KullaniciTuru admin değilse (1 değilse), login sayfasına yönlendirin
            if (kullaniciTuru != 1)
            {
                context.Result = new RedirectToActionResult("Index", "GirisYap", null);
            }
        }
    }
}