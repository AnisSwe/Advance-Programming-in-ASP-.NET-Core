using BLL.DTO;
using BLL.Services;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.MVC.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseService _expenseService;
        private readonly CategoryRepo _categoryRepo;

        public ExpenseController(ExpenseService expenseService, CategoryRepo categoryRepo)
        {
            _expenseService = expenseService;
            _categoryRepo = categoryRepo;
        }

        private int? GetUserId() => HttpContext.Session.GetInt32("UserId");

        private IActionResult RedirectIfNotLoggedIn()
        {
            if (GetUserId() == null) return RedirectToAction("Login", "Account");
            return null!;
        }

        public IActionResult Index()
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            var expenses = _expenseService.GetAll(GetUserId()!.Value);
            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExpenseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
                return View(dto);
            }
            dto.UserId = GetUserId()!.Value;
            _expenseService.Add(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            var dto = _expenseService.GetById(id);
            if (dto == null) return NotFound();
            ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name", dto.CategoryId);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(ExpenseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
                return View(dto);
            }
            dto.UserId = GetUserId()!.Value;
            _expenseService.Update(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dto = _expenseService.GetById(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _expenseService.Delete(id);
            return RedirectToAction("Index");
        }

        // Feature 1: Search & Filter
        [HttpGet]
        public IActionResult Search(int? categoryId, DateTime? from, DateTime? to, decimal? maxAmount)
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
            var results = _expenseService.Search(GetUserId()!.Value, categoryId, from, to, maxAmount);
            return View(results);
        }

        // Feature 2: Recurring Expenses
        public IActionResult Recurring()
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            var results = _expenseService.GetRecurring(GetUserId()!.Value);
            return View(results);
        }

        // Feature 3: Monthly Summary
        public IActionResult Summary(int? year, int? month)
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            int y = year ?? DateTime.Now.Year;
            int m = month ?? DateTime.Now.Month;
            var summary = _expenseService.GetMonthlySummary(GetUserId()!.Value, y, m);
            return View(summary);
        }
    }
}