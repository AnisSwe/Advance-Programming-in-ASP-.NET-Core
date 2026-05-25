/*using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class BudgetRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public BudgetRepo(ExpenseTrackerDbContext context) => _context = context;

        public List<Budget> GetByUser(int userId) =>
            _context.Budgets.Include(b => b.Category)
                .Where(b => b.UserId == userId).ToList();

        public Budget? GetByUserCategoryMonth(int userId, int categoryId, string monthYear)
        {
            var parts = monthYear.Split('-');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);

            return _context.Budgets
                .Include(b => b.Category)   // ← this was missing!
                .FirstOrDefault(b =>
                    b.UserId == userId &&
                    b.CategoryId == categoryId &&
                    b.MonthYear.StartsWith(year + "-") &&
                    (b.MonthYear == year + "-" + month ||
                     b.MonthYear == year + "-" + month.ToString("D2")));
        }
        public Budget? GetById(int id) => _context.Budgets.Find(id);

        public void Add(Budget budget)
        {
            _context.Budgets.Add(budget);
            _context.SaveChanges();
        }

        public void Update(Budget budget)
        {
            _context.Budgets.Update(budget);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var b = _context.Budgets.Find(id);
            if (b != null) { _context.Budgets.Remove(b); _context.SaveChanges(); }
        }
    }
}
*/
using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class BudgetRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public BudgetRepo(ExpenseTrackerDbContext context) => _context = context;

        public List<Budget> GetByUser(int userId) =>
            _context.Budgets.Include(b => b.Category)
                .Where(b => b.UserId == userId).ToList();

        public Budget? GetByUserCategoryMonth(int userId, int categoryId, string monthYear)
        {
            var parts = monthYear.Split('-');
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);

            return _context.Budgets
                .Include(b => b.Category)
                .FirstOrDefault(b =>
                    b.UserId == userId &&
                    b.CategoryId == categoryId &&
                    (b.MonthYear == year + "-" + month ||
                     b.MonthYear == year + "-" + month.ToString("D2")));
        }

        public Budget? GetById(int id) => _context.Budgets.Find(id);

        public void Add(Budget budget)
        {
            _context.Budgets.Add(budget);
            _context.SaveChanges();
        }

        public void Update(Budget budget)
        {
            _context.Budgets.Update(budget);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var b = _context.Budgets.Find(id);
            if (b != null) { _context.Budgets.Remove(b); _context.SaveChanges(); }
        }
    }
}