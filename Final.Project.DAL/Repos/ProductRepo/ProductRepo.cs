namespace Final.Project.DAL;
public class ProductRepo : GenericRepo<Product>
{
    public ProductRepo(ECommerceContext context) : base(context)
    {
    }
}
