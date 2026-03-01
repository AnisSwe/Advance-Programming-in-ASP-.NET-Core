using System.Diagnostics;
using FormProcessing.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormProcessing.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Registration() { 
            return View(new RegistrationModel());
        }
        [HttpPost]
        public IActionResult Registration(RegistrationModel obj) {
            if (ModelState.IsValid) {
                return RedirectToAction("Login");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Login() { 
            
            return View(new LoginModel());
        }
        [HttpPost]
        public IActionResult Login(LoginModel obj) {
            if (ModelState.IsValid) { //form validation
                return RedirectToAction("Privacy", "Home");
            }
            return View(obj);
        }
        //[HttpPost]
        //public IActionResult Login(string UName, string Pass)
        //{
        //    var uname = UName;
        //    var pass = Pass;

        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Login(IFormCollection data)
        //{
        //    var uname = data["UName"];
        //    var pass = data["Pass"];

        //    return View();
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
