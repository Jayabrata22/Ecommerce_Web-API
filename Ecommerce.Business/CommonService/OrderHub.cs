using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.CommonService
{
    public class OrderHub : Hub
    {
        // Optional: Send message to all clients (can be customized per seller ID)
        public async Task NotifySeller(string sellerId, string message)
        {
            await Clients.User(sellerId).SendAsync("ReceiveOrderNotification", message);
        }
    }
}
