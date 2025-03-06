using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.DTO
{
    public class CartDTO
    {
        public int BuyerId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }

}
