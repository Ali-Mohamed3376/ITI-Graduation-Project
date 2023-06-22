using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsCartController : ControllerBase
    {
        private readonly IUserProductsCartsManager _userProductsCartsManager;

        public UserProductsCartController(IUserProductsCartsManager userProductsCartsManager)
        {
            _userProductsCartsManager = userProductsCartsManager;
        }

        [HttpGet]
        public ActionResult GetAllProductsInCart()
        {
            //temp user id until we use authentication and then
            // we will get that userId from token by this function
            //var currentUser = _userManager.GetUserAsync(User).Result;
            //then get user id from current user details
            string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            IEnumerable<AllProductsReadDto> Products = _userProductsCartsManager.GetAllUserProductsInCart(userId);

            return Ok(Products);

        }

        [HttpPost]
        public ActionResult AddProductToCart(productToAddToCartDto product)
        {
            string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.AddProductToCart(product, userId);
            return Ok("product Added to Cart");
        }

        [HttpPut]
        public ActionResult UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product)
        {
            string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.UpdateProductQuantityInCart(product, userId);
            return Ok("product Quantity Edited to Cart");
        }


        

        [HttpDelete]
        public ActionResult DeleteProduct(UserProductInCartDeleteDto product)
        {
            string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.DeleteProductFromCart(product, userId);
            return Ok("product Deleted from Cart");
        }


    }
}
