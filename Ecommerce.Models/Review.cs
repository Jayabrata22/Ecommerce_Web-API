using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; } // 1-5 Stars
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Buyer { get; set; }
        public Product Product { get; set; }
    }

}
