using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProductbyID(int ProductId);
        Task<Product> AddProduct(Product product);
        Task<bool> DeleteProduct(int ProductId);
        Task<bool> UpdateProduct(Product product);
        Task<int> GetlastId();


    }
}
