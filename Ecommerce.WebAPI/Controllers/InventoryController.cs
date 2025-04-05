using Ecommerce.Business.Serviceinterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Authorize(Roles = "Seller")]
    [ApiController]
    [Route("api/seller/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IProductServiceInterface _service;
        public InventoryController(IProductServiceInterface service) => _service = service;

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int productId)
            => Ok(await _service.GetProductByIdAsync(productId));

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateStock(int productId, [FromBody] int quantity)
            => Ok(await _service.UpdateInventoryAsync(productId, quantity));
    }
}
