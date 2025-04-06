using Ecommerce.Models.DTO;
using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.CommonService
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(PaymentDto paymentDto);
        Task<IEnumerable<Payment>> GetCustomerPayments(string userId);
        Task<IEnumerable<Payment>> GetSellerPayments(string sellerId);
    }
}
