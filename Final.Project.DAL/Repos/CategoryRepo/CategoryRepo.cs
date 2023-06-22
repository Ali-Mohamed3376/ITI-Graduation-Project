namespace Final.Project.DAL;
public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
{
    public CategoryRepo(ECommerceContext context) : base(context)
    {
    }
}
