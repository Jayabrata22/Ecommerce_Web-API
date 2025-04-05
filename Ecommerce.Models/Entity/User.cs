using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Entity
{
    public class User : IdentityUser
    {
       
        public string Name { get; set; }      
        public string Password { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } // "Customer" or "Seller"
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Navigation properties
        //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
