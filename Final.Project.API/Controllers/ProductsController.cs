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

        #region Get all Products

        [HttpGet]
        [Route("dashboard")]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            IEnumerable<ProductReadDto> products = _productsManager.GetAllProducts();

            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        #endregion

        #region Add Product

        [HttpPost]
        [Route("dashboard")]
        public ActionResult Add(ProductAddDto productAddDto)
        {
            bool isAdded = _productsManager.AddProduct(productAddDto);
            return isAdded ? NoContent() : BadRequest();
        }

        #endregion

        #region Edit Product

        [HttpPut]
        [Route("dashboard")]
        public ActionResult Edit(ProductEditDto productEditDto)
        {
            bool isEdited = _productsManager.UpdateProduct(productEditDto);

            return isEdited ? NoContent() : BadRequest();
        }

        #endregion

        #region Delete Product

        [HttpDelete]
        [Route("dashboard")]
        public ActionResult Delete(int Id)
        {
            bool isDeleted = _productsManager.DeleteProduct(Id);

            return isDeleted ? NoContent() : BadRequest();
        }

        #endregion

    }
}






