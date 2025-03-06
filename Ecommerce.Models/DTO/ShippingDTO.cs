using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.DTO
{
    public class ShippingDTO
    {
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }

}
