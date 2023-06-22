using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsCartController : ControllerBase
    {
        [HttpGet]
        public ActionResult AddProductToCart(int id)
        {
            string? userId = "123";

            return Ok("product Added to Cart");
        }
    }
}
