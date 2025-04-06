using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(string userId);
        Task<IEnumerable<Payment>> GetPaymentsBySellerAsync(string sellerId);
    }
}
