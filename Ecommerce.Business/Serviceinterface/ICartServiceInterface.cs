using Ecommerce.Models.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Serviceinterface
{
    public  interface ICartServiceInterface
    {
        Task<CartDto> GetCartByUserIdAsync(string userId);
        Task AddItemAsync(string userId, int productId, int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task<CartDto> CreateCartAsync(string userId);
    }
}
