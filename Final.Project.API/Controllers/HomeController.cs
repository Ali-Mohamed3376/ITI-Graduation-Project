using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductsManager _productsManager;

        public HomeController(IProductsManager productsManager)
        {
            _productsManager = productsManager;
        }



        #region Get All Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductMiniDetailsDto>> GetAllProducts()
        {
            IEnumerable<ProductMiniDetailsDto> products = _productsManager.GetAllProducts();
            if (products == null) { return NotFound(); }
            return Ok(products);
        }
        #endregion

    }
}
