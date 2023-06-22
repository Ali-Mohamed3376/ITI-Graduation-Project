

using Final.Project.DAL;

namespace Final.Project.BL;

public class ProductsManager: IProductsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public ProductDetailsDto? GetProductByID(int id)
    {
        Product? productFromDb = _unitOfWork.ProductRepo.GetProductByIdWithCategory(id);
        if (productFromDb is null) { return null; }
        return new ProductDetailsDto()
        {
            Id = productFromDb.Id,
            Name = productFromDb.Name,
            Price = productFromDb.Price,
            Description = productFromDb.Description,
            Model = productFromDb.Model,
            Image = productFromDb.Image,
            CategoryName = productFromDb.Category.Name                      
        };
    }
}











