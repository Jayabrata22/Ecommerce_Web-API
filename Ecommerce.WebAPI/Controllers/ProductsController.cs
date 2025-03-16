using Ecommerce.Business.Interfaces.Service;
using Ecommerce.Models;
using Ecommerce.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var AllProducts = await _productService.GetAll();
            return Ok(AllProducts);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int ProductId)
        {
            var requestedProduct = await _productService.GetProductbyID(ProductId);
            if (requestedProduct != null)
            {
                return Ok(requestedProduct);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<Product> AddNewProduct(ProductDTO product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category,
                SellerId = 4,

            };
            var existingID = await _productService.GetlastId();
            if (existingID != 0)
            {
                newProduct.Id = existingID + 1;
            }
            else
            {
                newProduct.Id = 1;
            }
            var newProductDTO = new ProductDTO
            {
                
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                Category = newProduct.Category,
               
            };
            var result =  await _productService.AddProduct(newProductDTO);
            if(result != null)
            {
                return new Product
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Price = result.Price,
                    Stock = result.Stock,
                    Category = result.Category,
                    
                };
            }
            else
            {
                return null;
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int Id,[FromBody]ProductDTO product)
        {
            if (Id != product.Id)
            {
                return BadRequest();
            }
            var updateProduct = new Product
            {
                
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category,
                SellerId = 4
                
            };
            var result = await _productService.UpdateProduct(product);
            if(!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int ProductId)
        {
            var result = await _productService.DeleteProduct(ProductId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
