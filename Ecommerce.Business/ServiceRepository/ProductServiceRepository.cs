using AutoMapper;
using Ecommerce.Business.CommonService;
using Ecommerce.Business.Serviceinterface;
using Ecommerce.DataAccess.Interface.Customer;
using Ecommerce.DataAccess.Interface.Seller;
using Ecommerce.Models.DTO.ProductDTO;
using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.ServiceRepository
{
    public  class ProductServiceRepository : IProductServiceInterface
    {
        private readonly IMapper _mapper;
        private readonly IProductCusInterface _productCusInterface;
        private readonly IProductInterface _productInterface;
        private readonly SendMailOnLowInventory sendMailOnLowInventory;

        public ProductServiceRepository(IMapper mapper,IProductCusInterface productCusInterface,IProductInterface productInterface, SendMailOnLowInventory sendMailOnLowInventory)
        {
            _mapper = mapper;
            _productCusInterface = productCusInterface;
            _productInterface = productInterface;
            this.sendMailOnLowInventory = sendMailOnLowInventory;
        }
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(string category, string size, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productCusInterface.GetAllAsync(category, size, minPrice, maxPrice);
            await sendMailOnLowInventory.CheckLimitofInventory();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _productCusInterface.GetByIdAsync(id);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto, string sellerId)
        {
            var product = _mapper.Map<Product>(dto);
            product.SellerId = sellerId;
            product.Inventory = new Inventory { Quantity = dto.Quantity };
            var created = await _productInterface.AddAsync(product);
            return _mapper.Map<ProductResponseDto>(created);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductCreateDto dto)
        {
            var existing = await _productCusInterface.GetByIdAsync(id);
            if (existing == null) return null;
            existing = _mapper.Map(dto, existing);
            var updated = await _productInterface.UpdateAsync(existing);
            return _mapper.Map<ProductResponseDto>(updated);
        }

        public async Task<bool> DeleteProductAsync(int id) => await _productInterface.DeleteAsync(id);

        public async Task<bool> UpdateInventoryAsync(int productId, int quantity) => await _productInterface.UpdateInventoryAsync(productId, quantity);
    }

}

