using Ecommerce.Models.DTO;
using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface
{
    public  interface IUserInterface
    {
        Task<User> RegisterAsync(RegisterDTO model);
        Task<string> LoginAsync(LoginDTO model);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> AdminUpdatePasswordAsync(string userEmail, string newPassword);
        Task<bool> ConfirmEmailAndPhoneAsync(ConfirmUserContactDto dto);
    }
}
