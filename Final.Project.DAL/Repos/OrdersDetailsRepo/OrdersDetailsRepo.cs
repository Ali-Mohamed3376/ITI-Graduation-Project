namespace Final.Project.DAL;
public class OrdersDetailsRepo : GenericRepo<OrderProductDetails>, IOrdersDetailsRepo
{
    public OrdersDetailsRepo(ECommerceContext context) : base(context)
    {
    }
}
