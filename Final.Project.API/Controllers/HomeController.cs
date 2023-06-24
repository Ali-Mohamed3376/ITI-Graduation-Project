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
        private readonly ICategoriesManager _categoriesManager;


        public HomeController(IProductsManager productsManager, ICategoriesManager categoriesManager)
        {
            _productsManager = productsManager;
            _categoriesManager = categoriesManager;

        }



        #region Get All Products
        [HttpGet]
        [Route("AllProducts")]

        public ActionResult<IEnumerable<ProductChildDto>> GetAllProducts()
        {
            IEnumerable<ProductChildDto> products = _productsManager.GetAllProducts();
            if (products == null) { return NotFound(); }
            return Ok(products);
        }
        #endregion


        #region Get All Categories With All Products
        [HttpGet]
        [Route("CategoriesWProducts")]
        public ActionResult<IEnumerable<CategoryDetailsDto>> GetAllCategoriesWithProducts()
        {
            IEnumerable<CategoryDetailsDto> categories = _categoriesManager.GetAllCategoriesWithProducts();

            return Ok(categories);
        }

        #endregion

    }
}
