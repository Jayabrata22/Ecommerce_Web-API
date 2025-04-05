using Ecommerce.Models.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Serviceinterface
{
    public interface IOrderServiceInterface
    {
        Task<OrderDto> CreateOrderAsync(string userId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(string userId);
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, string status);    
    }
}
