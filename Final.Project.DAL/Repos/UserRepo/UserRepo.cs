namespace Final.Project.DAL;
public class UserRepo : GenericRepo<User>
{
    public UserRepo(ECommerceContext context) : base(context)
    {
    }
}
