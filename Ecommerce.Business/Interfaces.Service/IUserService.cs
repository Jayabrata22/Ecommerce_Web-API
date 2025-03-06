using Ecommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Interfaces.Service
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(UserDTO userDto);
        Task<string> LoginUserAsync(LogInDTO logindto);
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPasswordAsync(string token, string newPassword);
    }
}
