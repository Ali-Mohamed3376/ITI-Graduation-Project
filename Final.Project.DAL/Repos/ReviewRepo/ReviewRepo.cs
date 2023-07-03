

using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;

public class ReviewRepo : GenericRepo<Review>, IReviewRepo
{
    private readonly ECommerceContext _context;

    public ReviewRepo(ECommerceContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Review> GetReviewsByProduct(int productId)
    {
        return _context.Set<Review>()
            .Where(r => r.ProductId == productId)
            .ToList();
    }
}
