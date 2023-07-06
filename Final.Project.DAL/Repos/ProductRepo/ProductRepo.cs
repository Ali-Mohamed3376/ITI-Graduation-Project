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
    #region Get Product Details with images
    public Product? GetProductByIdWithimages(int id)
    {
        return _context.Set<Product>()
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p=>p.Id==id);
                    
                    
                    
    }

    #endregion

    #region Get All Products With AvgRating
    public IEnumerable<Product> GetAllProductsWithAvgRating()
    {
        return _context.Products.Include(p => p.Reviews);

    }
    #endregion

    #region Get All Products have Discount
    public IEnumerable<Product> GetAllProductWithDiscount()
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
            .Include(x=>x.Reviews)
            .Where(x => x.Category.Name == brand)
            .Take(5);

    }

    #endregion

    #region Get Product After Filteration

    public IEnumerable<Product> GetProductFiltered(QueryParametars parametars)
    {

        var products = _context.Products.Include(p=>p.Reviews).AsQueryable();
        
        if (parametars.CategotyId > 0)
        {
            products = products.Where(q => q.CategoryID == parametars.CategotyId);
        }

        if (parametars.ProductName != null || parametars.ProductName != "")
        {
            products = products.Where(q => q.Name.Contains(parametars.ProductName));
        }

        if (parametars.MaxPrice > 0)
        {
            products = products.Where(q => q.Price <= parametars.MaxPrice);
        }
        if (parametars.MinPrice > 0)
        {
            products = products.Where(q => q.Price >= parametars.MinPrice);
        }

        if (parametars.Rating > 0)
        {
            products = products.Where(q => q.Reviews.Average(r => r.Rating) >= parametars.Rating);
        }

        return products;
    }

    #endregion

}