namespace Final.Project.DAL;
public interface IProductRepo :IGenericRepo<Product>
{
    public Product? GetProductByIdWithCategory(int id);
    public IEnumerable<Product> GetAllProductsWithAvgRating();
    public IEnumerable<Product> GetAllProductWithDiscount();

    IEnumerable<Product> GetAllWithCategory();
    IEnumerable<Product> GetRelatedProductsByCategoryName(string brand);

    IEnumerable<Product> GetProductFiltered (QueryParametars parametars);
}
