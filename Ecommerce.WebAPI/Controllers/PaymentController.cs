using Ecommerce.Business.CommonService;
using Ecommerce.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Pay([FromBody] PaymentDto dto)
        {
            await _paymentService.ProcessPaymentAsync(dto);
            return Ok(new { message = "Payment successful" });
        }

        [HttpGet("customer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerPayments()
        {
            var userId = User.FindFirst("sub")?.Value;
            var payments = await _paymentService.GetCustomerPayments(userId);
            return Ok(payments);
        }

        [HttpGet("seller")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetSellerPayments()
        {
            var sellerId = User.FindFirst("sub")?.Value;
            var payments = await _paymentService.GetSellerPayments(sellerId);
            return Ok(payments);
        }
    }
}
