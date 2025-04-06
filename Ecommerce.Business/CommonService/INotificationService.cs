using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.CommonService
{
    public interface INotificationService
    {
        Task SendOrderSuccessNotificationAsync(string sellerId, string message);
        Task SendOrderPaymentNotificationToSeller(string sellerId, string orderId, decimal amount);
    }

}
