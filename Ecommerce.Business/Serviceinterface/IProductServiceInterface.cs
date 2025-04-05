using Ecommerce.Models.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Serviceinterface
{
    public interface IProductServiceInterface
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(string category, string size, decimal? minPrice, decimal? maxPrice);
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto, string sellerId);
        Task<ProductResponseDto> UpdateProductAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateInventoryAsync(int productId, int quantity);
    }
}
