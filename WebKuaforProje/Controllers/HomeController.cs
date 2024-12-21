using Microsoft.AspNetCore.Mvc;

namespace WebKuaforProje.Controllers
{
    public class HomeController : Controller
    {
        // Anasayfa
        public IActionResult Index()
        {
            return View();
        }

        // Hakkýmýzda sayfasý
        public IActionResult Hakkimizda()
        {
            return View();
        }

        // Fiyat listesi sayfasý
        public IActionResult Fiyatlar()
        {
            return View();
        }

        public IActionResult Randevu()
        {
            return View();
        }

    }
}
