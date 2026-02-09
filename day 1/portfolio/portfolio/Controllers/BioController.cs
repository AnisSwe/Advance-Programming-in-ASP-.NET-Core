using Microsoft.AspNetCore.Mvc;

namespace portfolio.Controllers
{
    public class BioController : Controller
    {
        public IActionResult Bio()
        {
            return View();
        }
    }
}
