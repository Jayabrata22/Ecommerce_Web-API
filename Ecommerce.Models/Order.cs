using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BuyerId { get; set; } // Foreign Key
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // Pending, Shipped, Delivered, Cancelled

        public User Buyer { get; set; } // Navigation Property
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
