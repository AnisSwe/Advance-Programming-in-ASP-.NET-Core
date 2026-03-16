using BookShop.EF;
using BookShop.EF.Tables;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        BookStoreDbContext db;
        public BooksController(BookStoreDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string searchString, decimal? minPrice, decimal? maxPrice)
        {
            var data = db.Books.ToList(); 

   
            ViewBag.PriceRangeBooks = db.Books.Where(b => b.Price >= 100 && b.Price <= 300).ToList();

            return View(data);
        }

        public IActionResult Details(int id)
        {
            var data = db.Books.Find(id); 
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book b)
        {
            db.Books.Add(b); //saves a insert query
            var rs = db.SaveChanges(); //commits the query return no of rows affected
            if (rs > 0)
            {
                TempData["Msg"] = b.Name + " Created Successfully";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = db.Books.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Book b)
        {
            var dbObj = db.Books.Find(b.Id);
            dbObj.Name = b.Name;
            dbObj.Isbn= b.Isbn;
            dbObj.Price = b.Price;
            dbObj.AuthorName = b.AuthorName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dbObj = db.Books.Find(id);
            db.Books.Remove(dbObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}