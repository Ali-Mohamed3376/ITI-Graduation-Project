namespace Final.Project.DAL;
public class OrdersDetailsRepo : GenericRepo<OrderProductDetails>
{
    public OrdersDetailsRepo(ECommerceContext context) : base(context)
    {
    }
}
