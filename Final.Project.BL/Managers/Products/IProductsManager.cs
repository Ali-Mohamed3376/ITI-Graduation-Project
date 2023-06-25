

namespace Final.Project.BL;

public interface IProductsManager
{
   public ProductDetailsDto GetProductByID(int id);
   IEnumerable<ProductChildDto> GetAllProductsWithAvgRating();
}
