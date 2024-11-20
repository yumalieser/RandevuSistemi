using Microsoft.AspNetCore.Mvc;

namespace RandevuSistemi.Controllers
{
    public class RandevuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
