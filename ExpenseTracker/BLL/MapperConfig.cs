using AutoMapper;
using BLL.DTO;
using DAL.EF.Tables;


namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<Expense, ExpenseDTO>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category != null ? s.Category.Name : ""))
                .ReverseMap()
                .ForMember(d => d.Category, o => o.Ignore()); // ← ignore navigation

            cfg.CreateMap<Budget, BudgetDTO>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category != null ? s.Category.Name : ""))
                .ReverseMap()
                .ForMember(d => d.Category, o => o.Ignore()); // ← ignore navigation

            cfg.CreateMap<Alert, AlertDTO>().ReverseMap();
            cfg.CreateMap<User, UserDTO>().ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}