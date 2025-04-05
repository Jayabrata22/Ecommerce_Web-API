using Ecommerce.Models.DTO.UserDTO;
using Ecommerce.Models.Entity;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAuthInterfaceService
    {
        Task<User> RegisterAsync(RegisterDTO model);
        Task<string> LoginAsync(LoginDTO model);
        Task<User> GetUserAsync(string email);
        Task<bool> UpdatePassword(User user, string newPassword);
        Task<bool> ConfirmEmailAndPhoneAsync(ConfirmUserContactDto dto);
        

    }
}
