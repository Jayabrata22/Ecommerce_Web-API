using Ecommerce.Business.Serviceinterface;
using Ecommerce.Models.DTO.ProductDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Authorize(Roles = "Seller")]
    [ApiController]
    [Route("api/seller/products")]
    public class SellerProductsController : ControllerBase
    {
        private readonly IProductServiceInterface _service;
        public SellerProductsController(IProductServiceInterface service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.CreateProductAsync(dto, sellerId);
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCreateDto dto)
            => Ok(await _service.UpdateProductAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteProductAsync(id));
    }

}
