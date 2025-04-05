using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface.Seller
{
    public interface IProductInterface
    {
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<Inventory> GetInventoryAsync(int productId);
        Task<bool> UpdateInventoryAsync(int productId, int quantity);
    }
}
