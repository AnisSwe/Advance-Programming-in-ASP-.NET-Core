using Efcore.EF;
using Efcore.EF.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Efcore.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentContext db;
        public DepartmentController(DepartmentContext db)
        {
            this.db = db;
        }

        // GET all
        public IActionResult Index()
        {
            var data = db.Departments.ToList();
            return View(data);
        }

        // GET one
        public IActionResult Details(int id)
        {
            var data = db.Departments.Find(id);
            return View(data);
        }

        // GET - show form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST - save to DB
        [HttpPost]
        public IActionResult Create(Department d)
        {
            db.Departments.Add(d);
            var rs = db.SaveChanges();
            if (rs > 0)
            {
                TempData["Msg"] = d.Name + " Created Successfully";
            }
            return RedirectToAction("Index");
        }
    }
}