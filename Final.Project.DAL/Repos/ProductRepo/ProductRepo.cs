namespace Final.Project.DAL;
public class ProductRepo : GenericRepo<Product>, IProductRepo
{
    public ProductRepo(ECommerceContext context) : base(context)
    {
    }
}
