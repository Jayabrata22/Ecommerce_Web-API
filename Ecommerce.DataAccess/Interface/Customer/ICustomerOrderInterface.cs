using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface.Customer
{
    public interface ICustomerOrderInterface
    {
        Task<Order> CreateOrderAsync(string userId);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}
