using Ecommerce.Business.Serviceinterface;
using Ecommerce.Models.DTO.Cart;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartServiceInterface _cartService;

        public CartController(ICartServiceInterface cartService) => _cartService = cartService;

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] string userId)
            => Ok(await _cartService.CreateCartAsync(userId));

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
            => Ok(await _cartService.GetCartByUserIdAsync(userId));

        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] CartItemDto item)
        {
            await _cartService.AddItemAsync(item.ProductId.ToString(), item.ProductId, item.Quantity);
            return Ok();
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _cartService.RemoveItemAsync(cartItemId);
            return Ok();
        }
    }
}
