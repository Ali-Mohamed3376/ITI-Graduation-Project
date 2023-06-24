namespace Final.Project.DAL;
public class OrdersDetailsRepo : GenericRepo<OrderProductDetails>, IOrdersDetailsRepo
{
    private readonly ECommerceContext _context;

    public OrdersDetailsRepo(ECommerceContext context) : base(context)
    {
        _context = context;
    }

    public void AddRange(IEnumerable<OrderProductDetails> orderProducts)
    {
        _context.Set<OrderProductDetails>()
            .AddRange(orderProducts);

    }
}
