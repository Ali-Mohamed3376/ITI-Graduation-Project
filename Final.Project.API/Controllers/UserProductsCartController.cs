using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsCartController : ControllerBase
    {
        //test by abdo
        private readonly IUserProductsCartsManager _userProductsCartsManager;
        private readonly UserManager<User> _manager;

        public UserProductsCartController(IUserProductsCartsManager userProductsCartsManager,
            UserManager<User> manager)
        {
            _userProductsCartsManager = userProductsCartsManager;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult GetAllProductsInCart()
        {
            //temp user id until we use authentication and then
            // we will get that userId from token by this function
            //var currentUser = _userManager.GetUserAsync(User).Result;
            //then get user id from current user details
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if(userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            //string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            IEnumerable<AllProductsReadDto> Products = _userProductsCartsManager.GetAllUserProductsInCart(userIdFromToken);

            return Ok(Products);

        }

        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProductToCart(productToAddToCartDto product)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
                
            }
            //string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.AddProductToCart(product, userIdFromToken);
            return Ok("product Added to Cart");
        }

        [HttpPut]
        [Route("UpdateProduct")]

        public ActionResult UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
           // string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.UpdateProductQuantityInCart(product, userIdFromToken);
            return Ok("product Quantity Edited to Cart");
        }


        

        [HttpDelete]
        [Route("DeleteProduct")]

        public ActionResult DeleteProduct(UserProductInCartDeleteDto product)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            //string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.DeleteProductFromCart(product, userIdFromToken);
            return Ok("product Deleted from Cart");
        }


    }
}
