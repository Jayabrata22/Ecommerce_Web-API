using Ecommerce.Business.Serviceinterface;
using Ecommerce.Business.ServiceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/seller/orders")]
    public class SellerOrdersController : ControllerBase
    {
        private readonly IOrderServiceInterface _orderService;

        public SellerOrdersController(IOrderServiceInterface orderService) => _orderService = orderService;

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
            => Ok(await _orderService.GetAllOrdersAsync());

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return Ok();
        }
    }
}
