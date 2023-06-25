using Final.Project.BL;
using Final.Project.DAL;
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
        private readonly ECommerceContext context;


        public HomeController(IProductsManager productsManager, ICategoriesManager categoriesManager, ECommerceContext context)
        {
            _productsManager = productsManager;
            _categoriesManager = categoriesManager;
            this.context = context;

        }



        #region Get All Products
        [HttpGet]
        [Route("AllProducts")]

        public ActionResult<IEnumerable<ProductChildDto>> GetAllProductsWithAvgRating()
        {
            IEnumerable<ProductChildDto> products = _productsManager.GetAllProductsWithAvgRating();
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
