using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; } // Shipped, In Transit, Delivered
        public DateTime EstimatedDelivery { get; set; }

        public Order Order { get; set; }
    }

}
