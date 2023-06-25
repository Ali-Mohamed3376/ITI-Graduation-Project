using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public class ProductRepo : GenericRepo<Product>, IProductRepo
{
    private readonly ECommerceContext _context;

    public ProductRepo(ECommerceContext context) : base(context)
    {
        _context = context;

    }

    #region Get Product Details with its Category
    public Product? GetProductByIdWithCategory(int id)
    {
        return _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Reviews)
                    .FirstOrDefault(p => p.Id == id);
    }

    #endregion

    #region Get All Products With AvgRating
    public IEnumerable<Product> GetAllProductsWithAvgRating()
    {
        return _context.Products.Include(p => p.Reviews);

    }
    #endregion


}