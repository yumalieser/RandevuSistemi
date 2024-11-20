using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RandevuSistemi.Filters
{
    public class UserAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var kullaniciTuru = context.HttpContext.Session.GetInt32("KullaniciTuru");

            // Eğer KullaniciTuru kullanıcı değilse (2 değilse), login sayfasına yönlendirin
            if (kullaniciTuru != 2)
            {
                context.Result = new RedirectToActionResult("Index", "GirisYap", null);
            }
        }
    }
}