using DAL.EF;
using DAL.EF.Tables;


namespace DAL.Repos
{
    public class UserRepo
    {
        private readonly ExpenseTrackerDbContext _context;
        public UserRepo(ExpenseTrackerDbContext context) => _context = context;

        public User? GetByEmail(string email) =>
            _context.Users.FirstOrDefault(u => u.Email == email);

        public User? GetById(int id) => _context.Users.Find(id);

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}