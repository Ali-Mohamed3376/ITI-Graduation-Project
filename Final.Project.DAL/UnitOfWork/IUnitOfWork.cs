namespace Final.Project.DAL;
public interface IUnitOfWork
{
    //public UserRepo UserRepo { get; }
    public IProductRepo ProductRepo { get; }
    public ICategoryRepo CategoryRepo { get; }
    public IOrderRepo OrderRepo { get; }
    public IOrdersDetailsRepo OrdersDetailsRepo  { get; }
    public IUserProductsCartRepo UserProdutsCartRepo { get; }
    public IUserAddressRepo UserAddressRepo { get; }
    int Savechanges();
}
