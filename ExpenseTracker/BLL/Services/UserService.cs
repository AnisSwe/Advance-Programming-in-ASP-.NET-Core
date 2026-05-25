using AutoMapper;
using BLL.DTO;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public bool Register(UserDTO dto)
        {
            var existing = _userRepo.GetByEmail(dto.Email);
            if (existing != null) return false; // email already exists

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _userRepo.Add(user);
            return true;
        }

        public UserDTO? Login(string email, string password)
        {
            var user = _userRepo.GetByEmail(email);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
            return _mapper.Map<UserDTO>(user);
        }
    }
}