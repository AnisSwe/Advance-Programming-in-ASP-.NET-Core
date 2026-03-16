using Microsoft.AspNetCore.Mvc;
using SchoolApp.EF;
using SchoolApp.EF.Tables;

namespace SchoolApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // READ ALL
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // READ ONE
        public IActionResult Details(int id)
        {
            var student = _context.Students.Find(id);
            return View(student);
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                TempData["Msg"] = student.Name + " Created Successfully";
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // EDIT GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            return View(student);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                TempData["Msg"] = student.Name + " Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                TempData["Msg"] = "Deleted Successfully";
            }
            return RedirectToAction("Index");
        }
    }
}
```

---

## PHASE 5 — Views

### Step 11 — Create Views folder
```
📁 Views
   └── 📁 Students
       ├── Index.cshtml
       ├── Create.cshtml
       ├── Edit.cshtml
       └── Details.cshtml