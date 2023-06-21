namespace Final.Project.DAL;
public interface IUnitOfWork
{
    public UserRepo UserRepo { get; }
    public ProductRepo ProductRepo { get; }
    public CategoryRepo CategoryRepo { get; }
    public OrderRepo OrderRepo { get; }
    public OrdersDetailsRepo OrdersDetailsRepo  { get; }
    public UserProdutsCartRepo UserProdutsCartRepo { get; }
    public UserAddressRepo UserAddressRepo { get; }
    int Savechanges();
}
