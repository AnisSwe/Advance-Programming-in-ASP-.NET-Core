using BLL.DTO;
using BLL.Services;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.MVC.Controllers
{
    public class BudgetController : Controller
    {
        private readonly BudgetService _budgetService;
        private readonly CategoryRepo _categoryRepo;

        public BudgetController(BudgetService budgetService, CategoryRepo categoryRepo)
        {
            _budgetService = budgetService;
            _categoryRepo = categoryRepo;
        }

        private int? GetUserId() => HttpContext.Session.GetInt32("UserId");

        public IActionResult Index()
        {
            if (GetUserId() == null) return RedirectToAction("Login", "Account");
            var budgets = _budgetService.GetByUser(GetUserId()!.Value);
            return View(budgets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(BudgetDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
                return View(dto);
            }
            dto.UserId = GetUserId()!.Value;
            _budgetService.Add(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dto = _budgetService.GetById(id);
            if (dto == null) return NotFound();
            ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name", dto.CategoryId);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(BudgetDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categoryRepo.GetAll(), "CategoryId", "Name");
                return View(dto);
            }
            dto.UserId = GetUserId()!.Value;
            _budgetService.Update(dto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _budgetService.Delete(id);
            return RedirectToAction("Index");
        }

        // Feature 4: Budget vs Actual Report
        public IActionResult Report(string? monthYear)
        {
            if (GetUserId() == null) return RedirectToAction("Login", "Account");
            monthYear ??= DateTime.Now.ToString("yyyy-MM");
            var report = _budgetService.GetReport(GetUserId()!.Value, monthYear);
            ViewBag.MonthYear = monthYear;
            return View(report);
        }
    }
}