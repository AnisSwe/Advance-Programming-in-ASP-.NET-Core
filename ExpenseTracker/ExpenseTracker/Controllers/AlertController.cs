using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.MVC.Controllers
{
    public class AlertController : Controller
    {
        private readonly AlertService _alertService;
        public AlertController(AlertService alertService) => _alertService = alertService;

        private int? GetUserId() => HttpContext.Session.GetInt32("UserId");

        // Feature 5: In-app Alerts
        public IActionResult Index()
        {
            if (GetUserId() == null) return RedirectToAction("Login", "Account");
            var alerts = _alertService.GetByUser(GetUserId()!.Value);
            _alertService.MarkAllRead(GetUserId()!.Value);
            return View(alerts);
        }
    }
}