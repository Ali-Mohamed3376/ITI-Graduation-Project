

using Final.Project.DAL;

namespace Final.Project.BL;

public class ProductsManager: IProductsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    #region Get All Products in Database
    public IEnumerable<ProductChildDto> GetAllProductsWithAvgRating()
    {
        IEnumerable<Product> productsFromDb = _unitOfWork.ProductRepo.GetAllProductsWithAvgRating();
        IEnumerable<ProductChildDto> productsDtos = productsFromDb
            .Select(p => new ProductChildDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Discount = p.Discount,
                AvgRating = p.Reviews.Any() ? (decimal)p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount=p.Reviews.Count()
                
            });
        return productsDtos;
    }

   

    #endregion


    #region Get Product Details
    public ProductDetailsDto? GetProductByID(int id)
    {
        Product? productFromDb = _unitOfWork.ProductRepo.GetProductByIdWithCategory(id);

        if (productFromDb is null) { return null; }
        return new ProductDetailsDto()
        {
            Id = productFromDb.Id,
            Name = productFromDb.Name,
            Price = productFromDb.Price,
            Discount= productFromDb.Discount,
            Description = productFromDb.Description,
            Model = productFromDb.Model,
            Image = productFromDb.Image,
            CategoryName = productFromDb.Category.Name,
            ReviewCount = productFromDb.Reviews.Count(),

            Reviews = productFromDb.Reviews.Select(p => new ReviewDto
            {
                Comment = p.Comment,
                CreationDate = p.CreationDate,
                Rating = p.Rating,

            }),
            AvgRating = (decimal)(productFromDb.Reviews.Any() ? productFromDb.Reviews.Average(r => r.Rating) : 0)



        };
    }
    #endregion

    #region GetAll Products Have discounts
    public IEnumerable<ProductChildDto> GetAllProductWithDiscount()
    {
       IEnumerable<Product>? productsFromDb=_unitOfWork.ProductRepo.GetAllProductWithDiscount();
        IEnumerable<ProductChildDto> productsDtos = productsFromDb
            .Select(p => new ProductChildDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Discount = p.Discount,
                AvgRating = p.Reviews.Any() ? (decimal)p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Reviews.Count()

            });
        return productsDtos;
    }
    #endregion

    #region Get All Products

    public IEnumerable<ProductReadDto> GetAllProducts()
    {
        IEnumerable<Product> productsFromDb = _unitOfWork.ProductRepo.GetAllWithCategory();
        if (productsFromDb is null)
        {
            return null;
        }

        IEnumerable<ProductReadDto> productReadDto = productsFromDb
            .Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                CategoryName = p.Category.Name
            });
        return productReadDto;
    }

    #endregion

    #region Add Product

    public bool AddProduct(ProductAddDto product)
    {
        Product ProductToAdd = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Image = product.Image,
            Model = product.Model,
            CategoryID = product.CategoryID,
        };

        _unitOfWork.ProductRepo.Add(ProductToAdd);
        return _unitOfWork.Savechanges() > 0;
    }

    #endregion

    #region Update Product

    public bool UpdateProduct(ProductEditDto productEditDto)
    {
        var product = _unitOfWork.ProductRepo.GetById(productEditDto.Id);
        if (product is null)
        {
            return false;
        }

        product.Name = productEditDto.Name;
        product.Price = productEditDto.Price;
        product.Description = productEditDto.Description;
        product.Image = productEditDto.Image;
        product.Model = productEditDto.Model;
        product.CategoryID = productEditDto.CategoryID;

        return _unitOfWork.Savechanges() > 0;
    }

    #endregion

    #region Delete Product

    public bool DeleteProduct(int Id)
    {
        var product = _unitOfWork.ProductRepo.GetById(Id);
        if (product is null)
        {
            return false;
        }

        _unitOfWork.ProductRepo.Delete(product);
        return _unitOfWork.Savechanges() > 0;
    }

    #endregion

}











