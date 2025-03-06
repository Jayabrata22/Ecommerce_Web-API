using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public interface IuserRepository
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> verifyUserAync(string email, string password);
        Task<bool> SetPasswordResetTokenAsync(string email, string token);
        Task<string> GetUserEmailByResetTokenAsync(string token);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
    }
}
