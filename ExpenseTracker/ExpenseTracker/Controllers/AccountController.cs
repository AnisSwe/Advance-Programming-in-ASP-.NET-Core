using BLL.DTO;
using BLL.Services;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        public AccountController(UserService userService) => _userService = userService;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.Login(email, password);
            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Index", "Expense");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(UserDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            bool success = _userService.Register(dto);
            if (!success)
            {
                ViewBag.Error = "Email already registered.";
                return View(dto);
            }
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}