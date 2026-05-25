using DAL.EF;
using DAL.EF.Tables;


namespace DAL.Repos
{
    public class CategoryRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public CategoryRepo(ExpenseTrackerDbContext context) => _context = context;

        public List<Category> GetAll() => _context.Categories.ToList();
    }
}