using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string SellerId { get; set; } // FK to Identity User
        public virtual User Seller { get; set; }

        public int CategoryId { get; set; }
        public Category Category    { get; set; }  // Navigation property
        public Inventory Inventory { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
