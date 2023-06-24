

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
    public IEnumerable<ProductChildDto> GetAllProducts()
    {
        IEnumerable<Product> productsFromDb = _unitOfWork.ProductRepo.GetAll();
        IEnumerable<ProductChildDto> productsDtos = productsFromDb
            .Select(p => new ProductChildDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Discount = p.Discount,
                
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
            CategoryName = productFromDb.Category.Name
        };
    }
    #endregion

}











