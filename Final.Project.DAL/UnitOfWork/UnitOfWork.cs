namespace Final.Project.DAL;
public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceContext context;

    public IUserRepo UserRepo { get; }
    public IProductRepo ProductRepo { get; }
    public ICategoryRepo CategoryRepo { get; }
    public IOrderRepo OrderRepo { get; }
    public IOrdersDetailsRepo OrdersDetailsRepo { get; }
    public IUserProductsCartRepo UserProdutsCartRepo { get; }
    public IUserAddressRepo UserAddressRepo { get; }

    public UnitOfWork(ECommerceContext context, IUserRepo userRepo, IProductRepo productRepo, ICategoryRepo categoryRepo, IOrderRepo orderRepo, IOrdersDetailsRepo ordersDetailsRepo, IUserProductsCartRepo userProdutsCartRepo, IUserAddressRepo userAddressRepo)
    {
        this.context = context;
        UserRepo = userRepo;
        ProductRepo = productRepo;
        CategoryRepo = categoryRepo;
        OrderRepo = orderRepo;
        OrdersDetailsRepo = ordersDetailsRepo;
        UserProdutsCartRepo = userProdutsCartRepo;
        UserAddressRepo = userAddressRepo;
    }

    public int Savechanges()
    {
        return context.SaveChanges();
    }
}
