

using Final.Project.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.BL;

public class ProductsManager: IProductsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    #region Get All Products in Database
   // public IEnumerable<ProductChildDto> GetAllProductsWithAvgRating()
   // {
   //     IEnumerable<Product> productsFromDb = _unitOfWork.ProductRepo.GetAllProductsWithAvgRating();
   //     IEnumerable<ProductChildDto> productsDtos = productsFromDb
   //         .Select(p => new ProductChildDto
   //         {
   //             Id = p.Id,
   //             Name = p.Name,
   //             Price = p.Price,
   //             Image = p.Image,
   //             Discount = p.Discount,
   //             AvgRating = p.Reviews.Any() ? (decimal)p.Reviews.Average(r => r.Rating) : 0,
   //             ReviewCount=p.Reviews.Count()
                
   //         });
   //     return productsDtos;
   // }

   ////version before making pagination

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
                FName=p.User.FName,
                LName=p.User.LName,
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
    public bool AddProduct(ProductAddDto productDto, string requestHost, string requestScheme)
    {


        var extension = Path.GetExtension(productDto.Image.FileName);
        // Save the file to disk
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(Environment.CurrentDirectory, "Images", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            productDto.Image.CopyToAsync(stream);
        }

        // Create a new Product object and save it to the database
        var ProductToAdd = new Product
        {
            Name = productDto.Name,
            //Image = $"{Request.Scheme}://{Request.Host}/Images/{fileName}",
            Image = $"{requestScheme}://{requestHost}/Image/{fileName}",
            Price = productDto.Price,
            CategoryID = productDto.CategoryID,
            Description = productDto.Description,
            Model = productDto.Model,
            Discount=productDto.Discount
        };
        _unitOfWork.ProductRepo.Add(ProductToAdd);
        return _unitOfWork.Savechanges() > 0;
    }

    #endregion

    #region Edit Product

    public bool EditProduct(ProductEditDto productEditDto)
    {
        var product = _unitOfWork.ProductRepo.GetById(productEditDto.Id);
        if (product is null)
        {
            return false;
        }

        product.Name = productEditDto.Name;
        product.Price = productEditDto.Price;
        product.Description = productEditDto.Description;
        product.Image = productEditDto == null ? product.Image : productEditDto.Image;
        product.Model = productEditDto.Model;
        product.CategoryID = productEditDto.CategoryID;
        product.Discount = productEditDto.Discount;

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

    #region Get Related Products by Category Name 
    public IEnumerable<RelatedProductDto> GetRelatedProducts(string brand)
    {
        IEnumerable<Product> productsFromDb = _unitOfWork.ProductRepo.GetRelatedProductsByCategoryName(brand);
        IEnumerable<RelatedProductDto> productsDtos = productsFromDb
            .Select(p => new RelatedProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                CategoryName=p.Category.Name,
                Discount = p.Discount,
                AvgRating = p.Reviews.Any() ? (decimal)p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount=p.Reviews.Count()
                


            });
        return productsDtos;
    }

    #endregion

    #region Filteration
    public IEnumerable<ProductFilterationResultDto> ProductAfterFilteration(ProductQueryDto queryDto)
    {
        var queryParameters = new QueryParametars
        {
            CategotyId = queryDto.CategotyId,
            ProductName = queryDto.ProductName,
            MaxPrice = queryDto.MaxPrice,
            MinPrice = queryDto.MinPrice,
            Rating = queryDto.Rating
        };

        var productsFilteredFromDB = _unitOfWork.ProductRepo.GetProductFiltered(queryParameters);


        IEnumerable<ProductFilterationResultDto> productsFilteredRsulte = productsFilteredFromDB.Select(p => new ProductFilterationResultDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Image = p.Image,
            Discount = p.Discount,
            AvgRating = p.Reviews.Any() ? (decimal)p.Reviews.Average(r => r.Rating) : 0,
            ReviewCount = p.Reviews.Count()
        });

        return productsFilteredRsulte;
    }


    #endregion

    #region Get All Products In Pagination
    public ProductPaginationDto GetAllProductsInPagnation(int page, int countPerPage)
    {
       var products= _unitOfWork.ProductRepo.GetAllProductsInPagnation(page, countPerPage)
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
        var totalCount = _unitOfWork.ProductRepo.GetCount();
        return new ProductPaginationDto
        {
            products = products,
            TotalCount = totalCount
        };
    }
    #endregion

    #region Filtering With Pagination

    public ProductFilterationPaginationResultDto ProductAfterFilterationInPagination(ProductQueryDto queryDto, int page, int countPerPage)
    {
        var queryParameters = new QueryParametars
        {
            CategotyId = queryDto.CategotyId,
            ProductName = queryDto.ProductName,
            MaxPrice = queryDto.MaxPrice,
            MinPrice = queryDto.MinPrice,
            Rating = queryDto.Rating
        };

        var productsFilteredFromDB = _unitOfWork.ProductRepo.GetProductFilteredInPagination(queryParameters,page,countPerPage);
        var totalCount = productsFilteredFromDB.Count();
        productsFilteredFromDB = productsFilteredFromDB
                    .Skip((page - 1) * countPerPage)
                    .Take(countPerPage);

        var productsFilteredResult = productsFilteredFromDB.Select(p => new ProductFilterationResultDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Image = p.Image,
            Discount = p.Discount,
            AvgRating = p.Reviews.Any() ? (decimal)p.Reviews.Average(r => r.Rating) : 0,
            ReviewCount = p.Reviews.Count()
        });

        return new ProductFilterationPaginationResultDto
        {
            filteredProducts = productsFilteredResult,
            TotalCount = totalCount
        };
    }
    #endregion



}











