using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.EF;
using SchoolManagement.EF.Tables;
using System.Linq;

namespace SchoolManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // ==================== READ OPERATIONS ====================

        // GET: Students - List all students (using LINQ)
        public IActionResult Index()
        {
            // LINQ: Select all students
            var students = (from s in _context.Students
                            select s).ToList();

            // Alternative LINQ method syntax
            // var students = _context.Students.ToList();

            return View(students);
        }

        // GET: Students/Details/5 - Get single student (using LINQ)
        public IActionResult Details(int id)
        {
            // LINQ: Find student by ID
            var student = (from s in _context.Students
                           where s.StudentId == id
                           select s).FirstOrDefault();

            // Alternative LINQ method syntax
            // var student = _context.Students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/GetByName/John - Search students by name (using LINQ)
        public IActionResult GetByName(string name)
        {
            // LINQ: Search students with name containing text
            var students = from s in _context.Students
                           where s.Name.Contains(name)
                           orderby s.Name
                           select s;

            return View(students);
        }

        // GET: Students/GetByAgeRange - Get students by age range (using LINQ)
        public IActionResult GetByAgeRange(int minAge, int maxAge)
        {
            // LINQ: Complex query with multiple conditions
            var students = from s in _context.Students
                           where s.Age >= minAge && s.Age <= maxAge
                           orderby s.Age descending
                           select new
                           {
                               StudentId = s.StudentId,
                               Name = s.Name,
                               Age = s.Age,
                               DateOfBirth = s.DateOfBirth
                           };

            return View(students.ToList());
        }

        // ==================== CREATE OPERATIONS ====================

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // LINQ: Add operation
                _context.Students.Add(student);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(student);
        }

        // POST: Students/CreateWithLinq - Alternative create with LINQ
        [HttpPost]
        public IActionResult CreateWithLinq(string name, int age)
        {
            // LINQ: Create new student object
            var newStudent = new Student
            {
                Name = name,
    
                Age = age
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ==================== UPDATE OPERATIONS ====================

        // GET: Students/Edit/5
        public IActionResult Edit(int id)
        {
            // LINQ: Find student to edit
            var student = (from s in _context.Students
                           where s.StudentId == id
                           select s).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // LINQ: Update operation
                    _context.Students.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // POST: Students/UpdateWithLinq - Alternative update with LINQ
        [HttpPost]
        public IActionResult UpdateWithLinq(int id, string name, int age)
        {
            // LINQ: Find and update
            var student = (from s in _context.Students
                           where s.StudentId == id
                           select s).FirstOrDefault();

            if (student != null)
            {
                // Update properties
                student.Name = name;
                student.Age = age;

                // LINQ: Save changes
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Students/UpdateMultiple - Update multiple students (LINQ)
        [HttpPost]
        public IActionResult UpdateMultiple(int minAge, int newAge)
        {
            // LINQ: Find multiple students and update
            var students = from s in _context.Students
                           where s.Age >= minAge
                           select s;

            foreach (var student in students)
            {
                student.Age = newAge;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ==================== DELETE OPERATIONS ====================

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            // LINQ: Find student to delete
            var student = (from s in _context.Students
                           where s.StudentId == id
                           select s).FirstOrDefault();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // LINQ: Find and delete
            var student = (from s in _context.Students
                           where s.StudentId == id
                           select s).FirstOrDefault();

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Students/DeleteMultiple - Delete multiple students (LINQ)
        [HttpPost]
        public IActionResult DeleteMultiple(int[] studentIds)
        {
            // LINQ: Delete multiple students
            var students = from s in _context.Students
                           where studentIds.Contains(s.StudentId)
                           select s;

            _context.Students.RemoveRange(students);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Students/DeleteByAge - Delete students by condition (LINQ)
        [HttpPost]
        public IActionResult DeleteByAge(int minAge)
        {
            // LINQ: Delete students meeting condition
            var students = from s in _context.Students
                           where s.Age >= minAge
                           select s;

            _context.Students.RemoveRange(students);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ==================== ADVANCED LINQ QUERIES ====================

        // GET: Students/AdvancedQueries
        public IActionResult AdvancedQueries()
        {
            // LINQ: Group by Age
            var groupByAge = from s in _context.Students
                             group s by s.Age into ageGroup
                             select new
                             {
                                 Age = ageGroup.Key,
                                 Count = ageGroup.Count(),
                                 Students = ageGroup.ToList()
                             };

            // LINQ: Join with other tables (if you have Courses/Enrollments)
            var studentCourses = from s in _context.Students
                                 join e in _context.Set<Enrollment>() on s.StudentId equals e.StudentId
                                 join c in _context.Set<Course>() on e.CourseId equals c.CourseId
                                 select new
                                 {
                                     StudentName = s.Name,
                                     CourseName = c.Title,
                                     e.Grade
                                 };

            // LINQ: Aggregation operations
            var stats = new
            {
                TotalStudents = _context.Students.Count(),
                AverageAge = _context.Students.Average(s => s.Age),
                MinAge = _context.Students.Min(s => s.Age),
                MaxAge = _context.Students.Max(s => s.Age),
                StudentsByName = _context.Students.OrderBy(s => s.Name).ToList()
            };

            return View(stats);
        }

        // ==================== HELPER METHODS ====================

        private bool StudentExists(int id)
        {
            // LINQ: Check if student exists
            return (from s in _context.Students
                    where s.StudentId == id
                    select s).Any();

            // Alternative
            // return _context.Students.Any(e => e.StudentId == id);
        }

        // GET: Students/Search - Search with multiple criteria (LINQ)
        public IActionResult Search(string name, int? minAge, int? maxAge)
        {
            // LINQ: Dynamic query building
            var query = from s in _context.Students
                        select s;

            if (!string.IsNullOrEmpty(name))
            {
                query = from s in query
                        where s.Name.Contains(name)
                        select s;
            }

            if (minAge.HasValue)
            {
                query = from s in query
                        where s.Age >= minAge.Value
                        select s;
            }

            if (maxAge.HasValue)
            {
                query = from s in query
                        where s.Age <= maxAge.Value
                        select s;
            }

            var results = query.ToList();
            return View(results);
        }
    }
}