namespace Final.Project.DAL;
public class CategoryRepo : GenericRepo<Category>
{
    public CategoryRepo(ECommerceContext context) : base(context)
    {
    }
}
