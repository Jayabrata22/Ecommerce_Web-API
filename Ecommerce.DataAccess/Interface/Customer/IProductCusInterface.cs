using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface.Customer
{
    public interface IProductCusInterface
    {
        Task<IEnumerable<Product>> GetAllAsync(string category, string size, decimal? minPrice, decimal? maxPrice);
        Task<Product> GetByIdAsync(int id);
    }
}
