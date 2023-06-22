using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsManager _productsManager;

        public ProductsController(IProductsManager productsManager)
        {
            _productsManager = productsManager;
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductDetailsDto> GetProductDetails(int id)
        {
            ProductDetailsDto product = _productsManager.GetProductByID(id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }
    }
}






