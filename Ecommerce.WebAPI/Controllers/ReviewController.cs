using Ecommerce.Business.Serviceinterface;
using Ecommerce.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // POST: /api/reviews
        [HttpPost]
        [Authorize(Roles = "Customer,Admin,Seller")]
        public async Task<IActionResult> SubmitReview([FromBody] ReviewDto reviewDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _reviewService.SubmitReviewAsync(reviewDto);
            return Ok(new { message = "Review submitted successfully." });
        }

        // GET: /api/reviews/{productId}
        [HttpGet("{productId}")]
        [Authorize(Roles = "Customer,Admin,Seller")]
        public async Task<IActionResult> GetReviews(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }
    }

}
