using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public int SellerId { get; set; } // Foreign Key

        public User Seller { get; set; } // Navigation Property
        public ICollection<OrderItem> OrderItems { get; set; }
       
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
