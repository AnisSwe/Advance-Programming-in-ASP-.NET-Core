using Microsoft.AspNetCore.Mvc;
using ProductInformation.Models;

namespace ProductInformation.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Pro", Price = 1299.99, Category = "Electronics" },
            new Product { Id = 2, Name = "Wireless Mouse", Price = 25.50, Category = "Accessories" },
            new Product { Id = 3, Name = "4K Monitor", Price = 499.99, Category = "Electronics" },
            new Product { Id = 4, Name = "Mechanical Keyboard", Price = 150.00, Category = "Accessories" },
            new Product { Id = 5, Name = "Gaming PC", Price = 2499.99, Category = "Electronics" }
        };

        // GET: /Product/Index (or just /Product)
        public IActionResult Index()
        {
            // Pass the product list to the view
            return View(_products);
        }

        // GET: /Product/Details/{id}
        public IActionResult Details(int id)
        {
            // Find the product by id
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Pass the single product to the view
            return View(product);
        }
    }
}

