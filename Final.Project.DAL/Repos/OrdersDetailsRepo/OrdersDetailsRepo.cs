using Microsoft.EntityFrameworkCore;

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


















    #region Get Top Products
    public IEnumerable<OrderProductDetails> GetTopProducts()
    {
        return _context.OrderProductDetails
                       .Include(op => op.Product)
                       .GroupBy(op => op.ProductId)
                       .Select(p => new OrderProductDetails
                       {
                           ProductId = p.Key,
                           Quantity = p.Sum(q => q.Quantity)
                       })
                       .OrderByDescending(q => q.Quantity)
                       .ToList();
    }

    #endregion










}
