using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

    #region Get All Products have Discount
    public IEnumerable<Product>GetAllProductWithDiscount()
    {
        return _context.Products
            .Include(p => p.Reviews)
            .Where(p => p.Discount > 0);
    }
    #endregion

    #region Get all With Category

    public IEnumerable<Product> GetAllWithCategory()
    {
        return _context.Set<Product>()
                       .Include(x => x.Category);
    }
    #endregion

    #region Get Related Products By brand
    public IEnumerable<Product> GetRelatedProductsByCategoryName(string brand)
    {
        return _context.Set<Product>()
            .Include(x => x.Category)
            .Where(x => x.Category.Name == brand)
            .Take(5);

    }

    public IEnumerable<Product> GetFilteredProducts(IQueryable<Product> query)
    {
      
        var products = _context.Products
                            .Include( p => p.Reviews)
                            .ToList();

        foreach (var product in products)
        {

            if (product.CategoryID > 0 )
            {
                query = query.Where(q => q.CategoryID == product.CategoryID);
            }

            if (!string.IsNullOrEmpty(product.Name))
            {
                query = query.Where(q => q.Name.Contains(product.Name));
            }

            if (product.Price > 0)
            {
                query = query.Where(q => q.Price <= product.Price);
            }

           
        }
        return products;

    }
    #endregion


}