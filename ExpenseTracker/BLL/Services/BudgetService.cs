using AutoMapper;
using BLL.DTO;
using DAL.EF.Tables;
using DAL.Repos;


namespace BLL.Services
{
    public class BudgetService
    {
        private readonly BudgetRepo _budgetRepo;
        private readonly ExpenseRepo _expenseRepo;
        private readonly IMapper _mapper;

        public BudgetService(BudgetRepo budgetRepo, ExpenseRepo expenseRepo)
        {
            _budgetRepo = budgetRepo;
            _expenseRepo = expenseRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public List<BudgetDTO> GetByUser(int userId)
        {
            var budgets = _budgetRepo.GetByUser(userId);
            var dtos = _mapper.Map<List<BudgetDTO>>(budgets);
            return dtos;
        }

        public BudgetDTO? GetById(int id)
        {
            var b = _budgetRepo.GetById(id);
            return b == null ? null : _mapper.Map<BudgetDTO>(b);
        }

        // Budget vs Actual report for a given month
        public List<BudgetDTO> GetReport(int userId, string monthYear)
        {
            var parts = monthYear.Split('-');
            int year = int.Parse(parts[0]), month = int.Parse(parts[1]);

            var budgets = _budgetRepo.GetByUser(userId)
                .Where(b => b.MonthYear == monthYear).ToList();

            var expenses = _expenseRepo.GetByMonth(userId, year, month);

            var dtos = _mapper.Map<List<BudgetDTO>>(budgets);
            foreach (var dto in dtos)
                dto.TotalSpent = expenses
                    .Where(e => e.CategoryId == dto.CategoryId)
                    .Sum(e => e.Amount);

            return dtos;
        }

        public void Add(BudgetDTO dto)
        {
            var budget = _mapper.Map<Budget>(dto);
            _budgetRepo.Add(budget);
        }

        public void Update(BudgetDTO dto)
        {
            var budget = _mapper.Map<Budget>(dto);
            _budgetRepo.Update(budget);
        }

        public void Delete(int id) => _budgetRepo.Delete(id);
    }
}