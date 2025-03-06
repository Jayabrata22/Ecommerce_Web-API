using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int BuyerId { get; set; } // Foreign Key

        public User Buyer { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }

}
