/*using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repos
{
    public class AlertRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public AlertRepo(ExpenseTrackerDbContext context) => _context = context;

        public List<Alert> GetByUser(int userId) =>
            _context.Alerts.Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt).ToList();

        public int GetUnreadCount(int userId) =>
            _context.Alerts.Count(a => a.UserId == userId && a.IsRead == false);

        public void Add(Alert alert)
        {
            _context.Alerts.Add(alert);
            _context.SaveChanges();
        }

        public void MarkAllRead(int userId)
        {
            var alerts = _context.Alerts
                .Where(a => a.UserId == userId && a.IsRead == false).ToList();
            foreach (var a in alerts) a.IsRead = true;
            _context.SaveChanges();
        }
    }
} */
using DAL.EF;
using DAL.EF.Tables;


namespace DAL.Repos
{
    public class AlertRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public AlertRepo(ExpenseTrackerDbContext context) => _context = context;

        public List<Alert> GetByUser(int userId) =>
            _context.Alerts.Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt).ToList();

        public int GetUnreadCount(int userId) =>
            _context.Alerts.Count(a => a.UserId == userId && a.IsRead == false);

        public void Add(Alert alert)
        {
            _context.Alerts.Add(alert);
            _context.SaveChanges();
        }

        public void MarkAllRead(int userId)
        {
            var alerts = _context.Alerts
                .Where(a => a.UserId == userId && a.IsRead == false).ToList();
            foreach (var a in alerts) a.IsRead = true;
            _context.SaveChanges();
        }
    }
}