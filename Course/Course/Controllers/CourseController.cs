using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Course.Models;
using System.Collections.Generic;
using System.Linq;

namespace Course.Controllers
{
    public class CourseController : Controller
    {
        private static List<Courses> courses = new List<Courses>
    {
        new Courses { Id = 1, Title = "Calculus I", Credit =3, Semester = "Fall" },
        new Courses { Id = 2, Title = "Physics I", Credit = 4, Semester = "Fall" },
        new Courses{ Id = 3, Title = "Data Structures", Credit = 3, Semester = "Spring" },
        new Courses { Id = 4, Title = "Database Systems", Credit = 3, Semester = "Spring" },
        new Courses { Id = 5, Title = "Algorithms", Credit = 3, Semester = "Summer" }
    };
        [Route("Course/List")]
        [Route("Course/List/{semester?}")]
        public IActionResult List(string Semester)
        {
            if (string.IsNullOrEmpty(Semester))
            {
                return View(courses);
            }
            else
            {
                var filteredCourses = courses
                    .Where(c => c.Semester.Equals(Semester, System.StringComparison.OrdinalIgnoreCase))
                .ToList();

                return View(filteredCourses);
            }
        }
    }
}

