using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesManager _categoriesManager;

        public CategoriesController(ICategoriesManager categoriesManager)
        {
            _categoriesManager = categoriesManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAllCategories()
        {
            IEnumerable<CategoryDto> categories = _categoriesManager.GetAllCategoriesDto();
            return Ok(categories);
        }


        [HttpGet]
        [Route("{id}")]

        public ActionResult<CategoryDto> GetCategoryById(int id)
        {
            CategoryDto? category = _categoriesManager.GetCategoryById(id);
            if (category is null) { return NotFound(); }

            return Ok(category);
        }




        [HttpGet]
        [Route("{id}/Products")]

        public ActionResult CategoryDetails(int id)
        {
            IEnumerable<ProductChildDto>? categoryDetailDto = _categoriesManager.GetCategoryWithProducts(id);
            if (categoryDetailDto == null) { return NotFound(); }
            return Ok(categoryDetailDto);
        }
    }
}
