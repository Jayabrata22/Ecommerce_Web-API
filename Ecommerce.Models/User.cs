using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class User
    {
        public string RestToken;

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Buyer", "Seller", or "Both"
        public DateTime CreatedAt { get; set; }

        // Buyer Relationships
        public ICollection<Order> Orders { get; set; }

        // Seller Relationships
        public ICollection<Product> Products { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
        public string ResetToken { get; set; }
    }

}
