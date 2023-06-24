using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public class UserProdutsCartRepo : GenericRepo<UserProductsCart>, IUserProductsCartRepo
{
    private readonly ECommerceContext _context;

    public UserProdutsCartRepo(ECommerceContext context) : base(context)
    {
        _context = context;

    }

    public void DeleteAllProductsFromUserCart(string userId)
    {
        _context.Set<UserProductsCart>()
                .Where(u => u.UserId == userId)
                .ExecuteDelete();
    }

    public IEnumerable<UserProductsCart> GetAllProductsByUserId(string userId)
    {
        var products = _context.Set<UserProductsCart>()
                .Include(u => u.Product)
                .Where(u => u.UserId == userId)
                .ToList();

        return products;
    }

    public UserProductsCart? GetByCompositeId(int ProductId, string userId)
    {
        return _context.Set<UserProductsCart>()
                        .Where(u => u.ProductId == ProductId && u.UserId == userId)
                        .FirstOrDefault();

    }


}
