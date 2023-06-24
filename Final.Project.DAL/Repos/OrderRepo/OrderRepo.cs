namespace Final.Project.DAL;
public class OrderRepo : GenericRepo<Order>, IOrderRepo
{
    private readonly ECommerceContext _context;
    public OrderRepo(ECommerceContext context) : base(context)
    {
        _context = context;
    }
    // select top(1) id from orders where userid=5 order by desc
    public int GetLastUserOrder(string userId)
    {
        return _context.Set<Order>()
                        .Where(o => o.UserId == userId)
                        .OrderByDescending(o => o.OrderDate)
                        .First().Id;

    }
}

