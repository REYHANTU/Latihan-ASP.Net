using Microsoft.AspNetCore.Mvc;
using SimpleLoginApp.Models;

namespace SimpleLoginApp.Controllers
{
    public class AuthController : Controller
    {
        // Simulasi user database
        private static List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "1234" },
            new User { Username = "reyhan", Password = "2003" }
        };

        // Halaman Login
        public IActionResult Login()
        {
            return View();
        }

        // Proses Login
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                // Validasi user
                var existingUser = _users.FirstOrDefault(u =>
                    u.Username == user.Username && u.Password == user.Password);

                if (existingUser != null)
                {
                    TempData["Message"] = "Login successful!";
                    return RedirectToAction("Dashboard");
                }

                TempData["Error"] = "Invalid username or password!";
            }

            return View(user);
        }

        // Halaman Dashboard
        public IActionResult Dashboard()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
