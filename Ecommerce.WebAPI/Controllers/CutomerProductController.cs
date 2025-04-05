using Ecommerce.Business.Serviceinterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServiceInterface _service;
        public ProductsController(IProductServiceInterface service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string category, string size, decimal? minPrice, decimal? maxPrice)
            => Ok(await _service.GetAllProductsAsync(category, size, minPrice, maxPrice));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetProductByIdAsync(id));
    }
}
