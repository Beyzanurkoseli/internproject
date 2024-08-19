using Microsoft.AspNetCore.Mvc;
using UniversiteOgrenciYonetimSistemi.Models;
using BCrypt.Net; // BCrypt kütüphanesi
using Microsoft.AspNetCore.Http; // Session için

namespace UniversiteOgrenciYonetimSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor, veritabanı bağlamını alır
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kayıt sayfasını görüntüler
        public IActionResult Register()
        {
            return View();
        }

        // Kayıt işlemini gerçekleştirir
        [HttpPost]
        public IActionResult Register(Ogrenci model)
        {
            // Modelin geçerli olup olmadığını kontrol eder
            if (ModelState.IsValid)
            {
                // Şifreyi hashleyerek güvenli bir şekilde saklar
                model.Sifre = BCrypt.Net.BCrypt.HashPassword(model.Sifre);

                // Yeni öğrenciyi veritabanına ekler
                _context.Ogrenciler.Add(model);
                _context.SaveChanges();

                // Başarılı kayıt sonrası mesaj ekleyin ve giriş sayfasına yönlendirin
                TempData["Message"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            // Model geçersizse, kayıt sayfasını tekrar gösterir
            return View(model);
        }

        // Giriş sayfasını görüntüler
        public IActionResult Login()
        {
            return View();
        }

        // Giriş işlemini gerçekleştirir
        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            // Girilen email ile veritabanında kullanıcıyı arar
            var ogrenci = _context.Ogrenciler.FirstOrDefault(o => o.Email == email);

            // Kullanıcı bulunursa ve şifre doğruysa
            if (ogrenci != null && BCrypt.Net.BCrypt.Verify(sifre, ogrenci.Sifre))
            {
                // Session'a kullanıcı bilgisini ekler
                HttpContext.Session.SetInt32("OgrenciId", ogrenci.Id);

                // Giriş başarılı olduğunda mesaj ekleyin ve profil sayfasına yönlendirin
                TempData["Message"] = "Login successful!";
                return RedirectToAction("Profile", "Account");
            }

            // Giriş başarısızsa hata mesajı ekler
            ModelState.AddModelError("", "Email veya şifre yanlış.");
            return View();
        }

        // Profil sayfasını görüntüler
        public IActionResult Profile()
        {
            // Session'dan öğrenci ID'sini alır
            var ogrenciId = HttpContext.Session.GetInt32("OgrenciId");

            // Öğrenci ID yoksa giriş sayfasına yönlendirir
            if (ogrenciId == null)
            {
                return RedirectToAction("Login");
            }

            // Belirtilen ID'ye sahip kullanıcıyı arar
            var ogrenci = _context.Ogrenciler.Find(ogrenciId);

            // Kullanıcı bulunduysa profil sayfasını gösterir ve mesajı ekrana getirir
            if (ogrenci != null)
            {
                ViewBag.Message = TempData["Message"];
                return View(ogrenci);
            }

            // Kullanıcı bulunamazsa giriş sayfasına yönlendirir
            return RedirectToAction("Login");
        }

        // Oturumu sonlandırır
        public IActionResult Logout()
        {
            // Session'ı temizler
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
