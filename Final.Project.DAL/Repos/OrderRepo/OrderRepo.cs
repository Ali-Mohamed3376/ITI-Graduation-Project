namespace Final.Project.DAL;
public class OrderRepo : GenericRepo<Order>, IOrderRepo
{
    public OrderRepo(ECommerceContext context) : base(context)
    {
    }
}
