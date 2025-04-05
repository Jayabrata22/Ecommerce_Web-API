using Ecommerce.Business.Serviceinterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class CustomerOrderController : ControllerBase
    {

        private readonly IOrderServiceInterface _orderService;

        public CustomerOrderController(IOrderServiceInterface orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] string userId)
            => Ok(await _orderService.CreateOrderAsync(userId));

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrderHistory(string userId)
            => Ok(await _orderService.GetOrdersByUserAsync(userId));

        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
            => Ok(await _orderService.GetOrderByIdAsync(orderId));
    }
}
