

using Final.Project.DAL;

namespace Final.Project.BL;

public interface IProductsManager
{
   public ProductDetailsDto GetProductByID(int id);
   IEnumerable<ProductChildDto> GetAllProductsWithAvgRating();

    public IEnumerable<ProductChildDto> GetAllProductWithDiscount();

    IEnumerable<ProductReadDto> GetAllProducts();
    bool AddProduct(ProductAddDto product);
    bool UpdateProduct(ProductEditDto productEditDto);
    bool DeleteProduct(int Id);
}
