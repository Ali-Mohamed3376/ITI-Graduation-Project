using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHelper _helper;

        public ProductsController(IProductsManager productsManager, ECommerceContext context,IHelper Helper)
        {
            _productsManager = productsManager;
            this.context = context;
            _helper = Helper;
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

        #region Products Filteration Old Version
        //[HttpPost]
        //[Route("Filter")]
        //public ActionResult GetAll(ProductQueryDto productQueryDto)
        //{
        //    var query = context.Products.AsQueryable();

        //    if (productQueryDto.CategotyId.HasValue && productQueryDto.CategotyId > 0)
        //    {
        //        query = query.Where(q => q.CategoryID == productQueryDto.CategotyId);
        //    }

        //    if (productQueryDto.ProductName != null || productQueryDto.ProductName != "")
        //    {
        //        query = query.Where(q => q.Name.Contains(productQueryDto.ProductName));
        //    }

        //    if (productQueryDto.MaxPrice.HasValue && productQueryDto.MaxPrice > 0)
        //    {
        //        query = query.Where(q => q.Price <= productQueryDto.MaxPrice.Value);
        //    }

        //    if (productQueryDto.MinPrice > 0)
        //    {
        //        query = query.Where(q => q.Price >= productQueryDto.MinPrice.Value);
        //    }

        //    if (productQueryDto.Rating.HasValue && productQueryDto.Rating > 0)
        //    {
        //        query = query.Where(q => q.Reviews.Average(r => r.Rating) >= productQueryDto.Rating.Value);
        //    }

        //    if (!query.Any())
        //    {
        //        return Ok("Not Found");
        //    }

        //    return Ok(query.ToList());
        //}

        #endregion

        #region Products Filteration
        [HttpPost]
        [Route("Filter")]
        public ActionResult GetAll(ProductQueryDto productQueryDto)
        {
            var result = _productsManager.ProductAfterFilteration(productQueryDto);

            return Ok(result.ToList());
        }
        #endregion


        #region Get all Products
        [HttpGet]
        [Route("Dashboard/GetAllProducts")]
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
        //[Authorize(Policy = "ForAdmin")]
        [HttpPost]
        [Route("Dashboard/AddProduct")]
        public ActionResult Add([FromForm] ProductAddDto product)
        {
            string message = _helper.ImageValidation(product.Image);

            if (message == "ok")
            {
                _productsManager.AddProduct(product, Request.Host.Value, Request.Scheme);
                return Ok(new { message = "ok" });
            }
            return BadRequest(message);
        }
        #endregion

        #region Edit Product
        [Authorize(Policy = "ForAdmin")]

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Product> Edit(ProductEditDto product, int id)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _productsManager.EditProduct(product);
            return Ok(product);

        }

        #endregion

        #region Delete Product
        [Authorize(Policy = "ForAdmin")]
        [HttpDelete]
        [Route("Dashboard/DeleteProduct/{Id}")]
        public ActionResult Delete(int Id)
        {
            bool isDeleted = _productsManager.DeleteProduct(Id);

            return isDeleted ? NoContent() : BadRequest();
        }

        #endregion

        #region Get Related Products
        [HttpGet]
        [Route("RelatedProducts/{brand}")]
        public ActionResult<RelatedProductDto> GetRelatedProducts(string brand)
        {
            IEnumerable<RelatedProductDto> products = _productsManager.GetRelatedProducts(brand);

            if (products is null)
            {
                return NotFound("There is No Related Products");
            }

            return Ok(products);
        }
        #endregion


        #region Get All Products in Pagination 
        [HttpGet]
        [Route("{page}/{countPerPage}")]

        public ActionResult<ProductPaginationDto> GetAllProductsInPagination(int page,int countPerPage)
        {
            return _productsManager.GetAllProductsInPagnation(page,countPerPage);
            
        }
        #endregion

        #region Filter With Pagination
        [HttpPost]
        [Route("PaginationFilter/{page}/{countPerPage}")]
        public ActionResult<ProductFilterationPaginationResultDto> GetAllProductAfterFilterationInPagination(ProductQueryDto productQueryDto, int page, int countPerPage)
        {
            var result = _productsManager.ProductAfterFilterationInPagination(productQueryDto, page, countPerPage);

            return Ok(result);
        }
        #endregion

    }
}