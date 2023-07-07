namespace Final.Project.DAL;
public interface IProductRepo :IGenericRepo<Product>
{
    public Product? GetProductByIdWithCategory(int id);
    public IEnumerable<Product> GetAllProductWithDiscount();

    IEnumerable<Product> GetAllWithCategory();
    IEnumerable<Product> GetRelatedProductsByCategoryName(string brand);

    IEnumerable<Product> GetProductFiltered (QueryParametars parametars);

    IEnumerable<Product> GetAllProductsInPagnation(int page, int countPerPage);
    int GetCount();
    IEnumerable<Product> GetProductFilteredInPagination(QueryParametars parametars, int page, int countPerPage);

    public IEnumerable<Product> GetNewProducts();

}
