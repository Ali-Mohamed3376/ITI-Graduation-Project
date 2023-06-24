using Final.Project.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsManager _reviewsManager;

        public ReviewsController(IReviewsManager reviewsManager)
        {
            _reviewsManager = reviewsManager;
        }
        [Authorize]
        [HttpPost("products/{productId}/reviews")]
        public IActionResult AddReview(int productId, [FromBody] ReviewDto reviewDto)
        {
            try
            {
                var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                _reviewsManager.AddReview(userIdFromToken, productId, reviewDto.Comment, reviewDto.Rating);

                return Ok("Review added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the review.");
            }
        }
    }
}
