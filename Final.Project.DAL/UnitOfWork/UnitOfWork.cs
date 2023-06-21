namespace Final.Project.DAL;
public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceContext context;

    public UserRepo UserRepo { get; }
    public ProductRepo ProductRepo { get; }
    public CategoryRepo CategoryRepo { get; }
    public OrderRepo OrderRepo { get; }
    public OrdersDetailsRepo OrdersDetailsRepo { get; }
    public UserProdutsCartRepo UserProdutsCartRepo { get; }
    public UserAddressRepo UserAddressRepo { get; }

    public UnitOfWork(ECommerceContext context, UserRepo userRepo, ProductRepo productRepo, CategoryRepo categoryRepo, OrderRepo orderRepo, OrdersDetailsRepo ordersDetailsRepo, UserProdutsCartRepo userProdutsCartRepo, UserAddressRepo userAddressRepo)
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
