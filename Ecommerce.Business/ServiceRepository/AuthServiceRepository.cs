using Business.Interfaces;
using Ecommerce.DataAccess.Implentation;
using Ecommerce.DataAccess.Interface;
using Ecommerce.Models.DTO.UserDTO;
using Ecommerce.Models.Entity;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthServiceRepository : IAuthInterfaceService
    {
        private readonly IUserInterface _authRepository;

        public AuthServiceRepository(IUserInterface UserAuthRepository)
        {
            _authRepository = UserAuthRepository;
        }

        public async Task<User> RegisterAsync(RegisterDTO model)
        {
            return await _authRepository.RegisterAsync(model);
        }

        public async Task<string> LoginAsync(LoginDTO model)
        {
            return await _authRepository.LoginAsync(model);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _authRepository.GetUserByEmailAsync(email);
        }
        public async Task<bool> UpdatePassword(User user, string newPassword)
        {
            return await _authRepository.AdminUpdatePasswordAsync(user.Email, newPassword);
        }

        public async Task<bool> ConfirmEmailAndPhoneAsync(ConfirmUserContactDto dto)
        {
            return  await _authRepository.ConfirmEmailAndPhoneAsync(dto);
        }
    }
}
