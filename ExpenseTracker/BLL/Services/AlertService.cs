using AutoMapper;
using BLL.DTO;
using DAL.EF.Tables;
using DAL.Repos;


namespace BLL.Services
{
    public class AlertService
    {
        private readonly AlertRepo _alertRepo;
        private readonly IMapper _mapper;

        public AlertService(AlertRepo alertRepo)
        {
            _alertRepo = alertRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public List<AlertDTO> GetByUser(int userId)
        {
            var alerts = _alertRepo.GetByUser(userId);
            return _mapper.Map<List<AlertDTO>>(alerts);
        }

        public int GetUnreadCount(int userId) => _alertRepo.GetUnreadCount(userId);

        public void MarkAllRead(int userId) => _alertRepo.MarkAllRead(userId);
    }
}