using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
{
    private readonly ECommerceContext _context;

    public CategoryRepo(ECommerceContext context) : base(context)
    {
        _context = context;

    }

    #region Get Category With  Products
    public IEnumerable<Product>? GetByIdWithProducts(int id)
    {
        return _context.Products
               .Include(p => p.Category)
               .Where(c => c.CategoryID == id).ToList();

    }
    #endregion
}













