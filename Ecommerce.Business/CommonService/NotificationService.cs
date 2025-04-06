using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.CommonService
{
    public class NotificationService : INotificationService
    {

        private readonly IHubContext<OrderHub> _hubContext;

        public NotificationService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendOrderSuccessNotificationAsync(string sellerId, string message)
        {
            await _hubContext.Clients.User(sellerId).SendAsync("ReceiveOrderNotification", message);
        }

        public async Task SendOrderPaymentNotificationToSeller(string sellerId, string orderId, decimal amount)
        {
            var message = $"Order #{orderId} was successfully paid. Amount: ₹{amount}";
            await _hubContext.Clients.User(sellerId).SendAsync("ReceiveOrderNotification", message);
        }
    }

}
