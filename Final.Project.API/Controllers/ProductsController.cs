using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsManager _productsManager;
        private readonly ECommerceContext context;
        public ProductsController(IProductsManager productsManager,ECommerceContext context)
        {
            _productsManager = productsManager;
            this.context = context;
        }

        #region Get Product By Id

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductDetailsDto> GetProductDetails(int id)
        {
            ProductDetailsDto product = _productsManager.GetProductByID(id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }
        #endregion

        #region Products Filteration by (CategoryName, ProductName, MinPrice, MaxPrice)

        [HttpGet("Filter")]
        public ActionResult GetAll([FromBody] ProductQueryDto productQueryDto)
        {
            var query = context.Products.AsQueryable();

            if (productQueryDto.CategotyId != 0)
            {
                query = query.Where(q => q.CategoryID == productQueryDto.CategotyId);
            }

            if (productQueryDto.ProductName != null)
            {
                query = query.Where(q => q.Name.Contains(productQueryDto.ProductName));
            }

            if (productQueryDto.MaxPrice != 0)
            {
                query = query.Where(q => q.Price <= productQueryDto.MaxPrice);
            }

            if (productQueryDto.MinPrice != 0)
            {
                query = query.Where(q => q.Price >= productQueryDto.MaxPrice);
            }

            if (productQueryDto.Rating != 0)
            {
                query = query.Where(q => q.Rating >= productQueryDto.Rating);
            }

            if (!query.Any())
            {
                return BadRequest("Not Found");
            }

            return Ok(query.ToList());
        }

        #endregion

        #region Products Filteration by Rating
       
        //[HttpGet("Product/Filter/ByRating")]
        //public IActionResult GetProductsByAverageRating(double minRating)
        //{
        //    var products = context.Products
        //        .GroupJoin(context.Reviews, p => p.Id, r => r.ProductId,
        //            (p, reviews) => new {
        //                p.Id,
        //                p.Name,
        //                p.Description,
        //                AverageRating = reviews.Average(r => r.Rating)
        //            })
        //        .Where(dto => dto.AverageRating >= minRating)
        //        .Select(dto => new ProductDto
        //        {
        //            ProductId = dto.ProductId,
        //            Name = dto.Name,
        //            Description = dto.Description,
        //            AverageRating = dto.AverageRating
        //        })
        //        .ToList();

        //    return Ok(products);
        //}
        
        #endregion
    }
}






