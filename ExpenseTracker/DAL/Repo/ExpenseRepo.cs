using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class ExpenseRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public ExpenseRepo(ExpenseTrackerDbContext context) => _context = context;

        public List<Expense> GetAllByUser(int userId) =>
            _context.Expenses.Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date).ToList();

        public Expense? GetById(int id) =>
            _context.Expenses.Include(e => e.Category)
                .FirstOrDefault(e => e.ExpenseId == id);

        public void Add(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        
        }

        public void Update(Expense expense)
        {
            _context.Expenses.Update(expense);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
        }

        public List<Expense> Search(int userId, int? categoryId, DateTime? from, DateTime? to, decimal? maxAmount)
        {
            var query = _context.Expenses.Include(e => e.Category)
                .Where(e => e.UserId == userId).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(e => e.CategoryId == categoryId);
            if (from.HasValue)
                query = query.Where(e => e.Date >= DateOnly.FromDateTime(from.Value));
            if (to.HasValue)
                query = query.Where(e => e.Date <= DateOnly.FromDateTime(to.Value));
            if (maxAmount.HasValue)
                query = query.Where(e => e.Amount <= maxAmount);

            return query.OrderByDescending(e => e.Date).ToList();
        }

        public List<Expense> GetRecurring(int userId) =>
            _context.Expenses.Include(e => e.Category)
                .Where(e => e.UserId == userId && e.IsRecurring == true).ToList();

        public List<Expense> GetByMonth(int userId, int year, int month) =>
            _context.Expenses.Include(e => e.Category)
                .Where(e => e.UserId == userId && e.Date.Year == year && e.Date.Month == month).ToList();
    }
}