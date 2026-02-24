using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            var employees = new List<Employee>
            {
                new Employee { Name = "Alice Johnson", Salary = 75000, Designation = "Manager" },
                new Employee { Name = "Bob Smith", Salary = 45000, Designation = "Developer" },
                new Employee { Name = "Carol White", Salary = 55000, Designation = "Senior Developer" },
                new Employee { Name = "David Brown", Salary = 90000, Designation = "Manager" },
                new Employee { Name = "Eve Davis", Salary = 35000, Designation = "Intern" },
                new Employee { Name = "Frank Miller", Salary = 52000, Designation = "Analyst" }
            };

            // Pass list to view
            return View(employees);
        }
    }
}
