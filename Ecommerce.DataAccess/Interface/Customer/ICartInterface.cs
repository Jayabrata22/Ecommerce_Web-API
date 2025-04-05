using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface.Customer
{
    public interface ICartInterface
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddItemAsync(string userId, int productId, int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task<Cart> CreateCartAsync(string userId);
    }
}
