using Microsoft.AspNetCore.Mvc;
using WebKuaforProje.Models;
using Microsoft.EntityFrameworkCore;

namespace WebKuaforProje.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Login GET
        public IActionResult Login()
        {
            return View();
        }

        // Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı e-posta ve parola ile kontrol et
                var musteri = await _context.Musteriler
                    .FirstOrDefaultAsync(m => m.Email == email && m.PhoneNumber == password);  // Şifreyi phoneNumber ile kontrol ediyorsunuz, buna göre düzeltme yapabilirsiniz.

                if (musteri != null)
                {
                    // Başarılı giriş
                    TempData["SuccessMessage"] = $"Hoş geldiniz, {musteri.FirstName} {musteri.LastName}!";

                    // Admin veya müşteri rolü kontrolü
                    if (musteri.Role == "Admin")
                    {
                        // Admin girişi
                        HttpContext.Session.SetString("Role", "Admin");
                    }
                    else
                    {
                        // Müşteri girişi
                        HttpContext.Session.SetString("Role", "Musteri");
                    }

                    return RedirectToAction("Index", "Home");
                }

                // Hatalı giriş
                TempData["ErrorMessage"] = "Geçersiz e-posta veya parola!";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "Lütfen tüm alanları doldurun.";
            return RedirectToAction("Login");
        }

        // Register GET
        public IActionResult Register()
        {
            return View();
        }

        // Register POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Musteri model, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Parola eşleşmesi kontrolü
                if (model.PhoneNumber != confirmPassword)
                {
                    TempData["ErrorMessage"] = "Parolalar eşleşmiyor!";
                    return View(model);
                }

                // E-posta ile müşteri kontrolü
                var existingMusteri = await _context.Musteriler.FirstOrDefaultAsync(m => m.Email == model.Email);
                if (existingMusteri != null)
                {
                    TempData["ErrorMessage"] = "Bu e-posta ile zaten bir hesap var!";
                    return View(model);
                }

                // Yeni müşteri oluştur
                model.Role = "Musteri"; // Varsayılan rol müşteri olarak atanıyor
                _context.Musteriler.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kaydınız başarıyla tamamlandı! Şimdi oturum açabilirsiniz.";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "Kayıt başarısız! Lütfen bilgilerinizi kontrol edin.";
            return View(model);
        }
    }
}
