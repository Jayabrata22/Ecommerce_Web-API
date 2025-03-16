using Ecommerce.Models;
using Ecommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Interfaces.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetProductbyID(int ProductId);
        Task<ProductDTO> AddProduct(ProductDTO product);
        Task<bool> DeleteProduct(int ProductId);
        Task<bool> UpdateProduct(ProductDTO product);

        Task<int> GetlastId();
    }
}
