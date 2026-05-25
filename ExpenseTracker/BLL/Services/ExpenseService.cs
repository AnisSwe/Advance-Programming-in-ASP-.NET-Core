/**using AutoMapper;
using ExpenseTracker.BLL;
using BLL.DTO;
using DAL.EF.Tables;
using DAL.Repos;
namespace BLL.Services
{
    public class ExpenseService
    {
        private readonly ExpenseRepo _expenseRepo;
        private readonly BudgetRepo _budgetRepo;
        private readonly AlertRepo _alertRepo;
        private readonly IMapper _mapper;

        public ExpenseService(ExpenseRepo expenseRepo, BudgetRepo budgetRepo, AlertRepo alertRepo)
        {
            _expenseRepo = expenseRepo;
            _budgetRepo = budgetRepo;
            _alertRepo = alertRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public List<ExpenseDTO> GetAll(int userId)
        {
            var expenses = _expenseRepo.GetAllByUser(userId);
            return _mapper.Map<List<ExpenseDTO>>(expenses);
        }

        public ExpenseDTO? GetById(int id)
        {
            var expense = _expenseRepo.GetById(id);
            return expense == null ? null : _mapper.Map<ExpenseDTO>(expense);
        }

        public void Add(ExpenseDTO dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            _expenseRepo.Add(expense);
            CheckBudgetAndAlert(dto.UserId, dto.CategoryId, dto.Date);
        }

        public void Update(ExpenseDTO dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            _expenseRepo.Update(expense);
            CheckBudgetAndAlert(dto.UserId, dto.CategoryId, dto.Date);
        }

        public void Delete(int id) => _expenseRepo.Delete(id);

        public List<ExpenseDTO> Search(int userId, int? categoryId, DateTime? from, DateTime? to, decimal? maxAmount)
        {
            var results = _expenseRepo.Search(userId, categoryId, from, to, maxAmount);
            return _mapper.Map<List<ExpenseDTO>>(results);
        }

        public List<ExpenseDTO> GetRecurring(int userId)
        {
            var results = _expenseRepo.GetRecurring(userId);
            return _mapper.Map<List<ExpenseDTO>>(results);
        }

        public SummaryDTO GetMonthlySummary(int userId, int year, int month)
        {
            var expenses = _expenseRepo.GetByMonth(userId, year, month);
            if (!expenses.Any()) return new SummaryDTO { MonthYear = $"{year}-{month:D2}" };

            var topCategory = expenses
                .GroupBy(e => e.Category?.Name ?? "Other")
                .OrderByDescending(g => g.Sum(e => e.Amount))
                .First().Key;

            int daysInMonth = DateTime.DaysInMonth(year, month);

            return new SummaryDTO
            {
                TotalSpent = expenses.Sum(e => e.Amount),
                DailyAverage = expenses.Sum(e => e.Amount) / daysInMonth,
                TopCategory = topCategory,
                TotalTransactions = expenses.Count,
                MonthYear = $"{year}-{month:D2}"
            };
        }

        // Auto alert if budget exceeded after adding/updating expense
        /* private void CheckBudgetAndAlert(int userId, int categoryId, DateOnly date)
         {
             string monthYear = $"{date.Year}-{date.Month:D2}";
             var budget = _budgetRepo.GetByUserCategoryMonth(userId, categoryId, monthYear);
             if (budget == null) return;

             var expenses = _expenseRepo.GetByMonth(userId, date.Year, date.Month);
             decimal totalSpent = expenses.Where(e => e.CategoryId == categoryId).Sum(e => e.Amount);

             if (totalSpent > budget.LimitAmount)
             {
                 _alertRepo.Add(new Alert
                 {
                     UserId = userId,
                     Message = $"Budget exceeded for {budget.Category?.Name ?? "category"} in {monthYear}! Spent {totalSpent} BDT, limit was {budget.LimitAmount} BDT.",
                     IsRead = false,
                     CreatedAt = DateTime.Now
                 });
             }
         } resrve code for duplicate alert check 
private void CheckBudgetAndAlert(int userId, int categoryId, DateOnly date)
        {
            string monthYear = $"{date.Year}-{date.Month:D2}";

            var budget = _budgetRepo.GetByUserCategoryMonth(userId, categoryId, monthYear);
            if (budget == null) return;

            var expenses = _expenseRepo.GetByMonth(userId, date.Year, date.Month);
            decimal totalSpent = expenses
                .Where(e => e.CategoryId == categoryId)
                .Sum(e => e.Amount);

            if (totalSpent > budget.LimitAmount)
            {
                string categoryName = budget.Category?.Name ?? "Unknown";

                // avoid duplicate alerts
                var existingAlerts = _alertRepo.GetByUser(userId);
                bool alreadyAlerted = existingAlerts.Any(a =>
                    a.Message.Contains(categoryName) &&
                    a.Message.Contains(monthYear));

                if (!alreadyAlerted)
                {
                    _alertRepo.Add(new Alert
                    {
                        UserId = userId,
                        Message = $"Budget exceeded for {categoryName} in {monthYear}! Spent {totalSpent} BDT, limit was {budget.LimitAmount} BDT.",
                        IsRead = false,
                        CreatedAt = DateTime.Now
                    });
                }
            }
        }
    }
}
*/
using AutoMapper;
using BLL.DTO;
using DAL.EF.Tables;
using DAL.Repos;




namespace BLL.Services
{
    public class ExpenseService
    {
        private readonly ExpenseRepo _expenseRepo;
        private readonly BudgetRepo _budgetRepo;
        private readonly AlertRepo _alertRepo;
        private readonly IMapper _mapper;

        public ExpenseService(ExpenseRepo expenseRepo, BudgetRepo budgetRepo, AlertRepo alertRepo)
        {
            _expenseRepo = expenseRepo;
            _budgetRepo = budgetRepo;
            _alertRepo = alertRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public List<ExpenseDTO> GetAll(int userId)
        {
            var expenses = _expenseRepo.GetAllByUser(userId);
            return _mapper.Map<List<ExpenseDTO>>(expenses);
        }

        public ExpenseDTO? GetById(int id)
        {
            var expense = _expenseRepo.GetById(id);
            return expense == null ? null : _mapper.Map<ExpenseDTO>(expense);
        }

        public void Add(ExpenseDTO dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            _expenseRepo.Add(expense);
            CheckBudgetAndAlert(dto.UserId, dto.CategoryId, dto.Date);
        }

        public void Update(ExpenseDTO dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            _expenseRepo.Update(expense);
            CheckBudgetAndAlert(dto.UserId, dto.CategoryId, dto.Date);
        }

        public void Delete(int id) => _expenseRepo.Delete(id);

        public List<ExpenseDTO> Search(int userId, int? categoryId, DateTime? from, DateTime? to, decimal? maxAmount)
        {
            var results = _expenseRepo.Search(userId, categoryId, from, to, maxAmount);
            return _mapper.Map<List<ExpenseDTO>>(results);
        }

        public List<ExpenseDTO> GetRecurring(int userId)
        {
            var results = _expenseRepo.GetRecurring(userId);
            return _mapper.Map<List<ExpenseDTO>>(results);
        }

        public SummaryDTO GetMonthlySummary(int userId, int year, int month)
        {
            var expenses = _expenseRepo.GetByMonth(userId, year, month);
            if (!expenses.Any()) return new SummaryDTO { MonthYear = $"{year}-{month:D2}" };

            var topCategory = expenses
                .GroupBy(e => e.Category?.Name ?? "Other")
                .OrderByDescending(g => g.Sum(e => e.Amount))
                .First().Key;

            int daysInMonth = DateTime.DaysInMonth(year, month);

            return new SummaryDTO
            {
                TotalSpent = expenses.Sum(e => e.Amount),
                DailyAverage = expenses.Sum(e => e.Amount) / daysInMonth,
                TopCategory = topCategory,
                TotalTransactions = expenses.Count,
                MonthYear = $"{year}-{month:D2}"
            };
        }

        private void CheckBudgetAndAlert(int userId, int categoryId, DateOnly date)
        {
            string monthYear = $"{date.Year}-{date.Month:D2}";

            var budget = _budgetRepo.GetByUserCategoryMonth(userId, categoryId, monthYear);
            if (budget == null) return;

            var expenses = _expenseRepo.GetByMonth(userId, date.Year, date.Month);
            decimal totalSpent = expenses
                .Where(e => e.CategoryId == categoryId)
                .Sum(e => e.Amount);

            if (totalSpent > budget.LimitAmount)
            {
                string categoryName = budget.Category?.Name ?? "Unknown";

                // avoid duplicate alerts
                var existingAlerts = _alertRepo.GetByUser(userId);
                bool alreadyAlerted = existingAlerts.Any(a =>
                    a.Message.Contains(categoryName) &&
                    a.Message.Contains(monthYear));

                if (!alreadyAlerted)
                {
                    _alertRepo.Add(new Alert
                    {
                        UserId = userId,
                        Message = $"Budget exceeded for {categoryName} in {monthYear}! Spent {totalSpent} BDT, limit was {budget.LimitAmount} BDT.",
                        IsRead = false,
                        CreatedAt = DateTime.Now
                    });
                }
            }
        }
    }
}