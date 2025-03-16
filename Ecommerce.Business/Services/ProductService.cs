using AutoMapper;
using Ecommerce.Business.Interfaces.Service;
using Ecommerce.DataAccess.Interfaces;
using Ecommerce.Models;
using Ecommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Services
{
    public class ProductService : IProductService 
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository , IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> AddProduct(ProductDTO product)
        {
            var products = _mapper.Map<Product>(product);
            var addNewProduct =  await _productRepository.AddProduct(products);
            return _mapper.Map<ProductDTO>(addNewProduct);
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            return await  _productRepository.DeleteProduct(ProductId);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var ProductsComing = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(ProductsComing);
        }

        public Task<int> GetlastId()
        {
            return _productRepository.GetlastId();
        }

        public async Task<ProductDTO> GetProductbyID(int ProductId)
        {
            var requestProduct = await _productRepository.GetProductbyID(ProductId);
            return _mapper.Map<ProductDTO>(requestProduct);
        }

        public Task<bool> UpdateProduct(ProductDTO product)
        {
            var updateProduct = _mapper.Map<Product>(product);
            return _productRepository.UpdateProduct(updateProduct);
        }
    }
}
