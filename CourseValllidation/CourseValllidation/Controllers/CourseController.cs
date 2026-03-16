using Microsoft.AspNetCore.Mvc;

public class CourseController : Controller
{
    // Fake database
    private static List<CourseModel> _courses = new List<CourseModel>();

    // ─────────────────────────────────────
    // GET: /Course/Register
    // ─────────────────────────────────────
    [HttpGet]
    public IActionResult Register()
    {
        return View(new CourseModel());
    }

    // ─────────────────────────────────────
    // POST: /Course/Register
    // ─────────────────────────────────────
    [HttpPost]
    public IActionResult Register(CourseModel model)
    {
        // ✅ All annotations checked automatically here
        if (!ModelState.IsValid)
        {
            return View(model); // ❌ return with errors
        }

        // Assign ID and save
        model.Id = _courses.Count + 1;
        _courses.Add(model);

        return RedirectToAction("Success");
    }

    // ─────────────────────────────────────
    // GET: /Course/List
    // GET: /Course/List/Fall-2024
    // ─────────────────────────────────────
    [HttpGet]
    public IActionResult List(string? semester = null)
    {
        var result = semester == null
            ? _courses                                          // show all
            : _courses.Where(c => c.Semester == semester).ToList(); // filter

        ViewBag.Semester = semester;
        return View(result);
    }

    public IActionResult Success()
    {
        return View();
    }
}