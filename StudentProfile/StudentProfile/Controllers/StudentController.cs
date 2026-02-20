using Microsoft.AspNetCore.Mvc;
using StudentProfile.Models;

namespace StudentProfile.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Profile(int id)
        {
            var students = new List<Student>
    {
        new Student { Id = 1, Name = "John Doe", Age = 17, Department = "CS", CGPA = 3.8 },
        new Student { Id = 2, Name = "Jane Smith", Age = 20, Department = "IT", CGPA = 3.2 },
        new Student { Id =3,Name = "MUnna", Age = 25 ,Department = "ICE", CGPA = 3.97}
    };

            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }
    }
}
