namespace Final.Project.DAL;
public class OrderRepo : GenericRepo<Order>
{
    public OrderRepo(ECommerceContext context) : base(context)
    {
    }
}
