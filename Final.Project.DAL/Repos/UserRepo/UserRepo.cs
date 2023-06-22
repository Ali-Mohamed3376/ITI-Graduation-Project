namespace Final.Project.DAL;
public class UserRepo : GenericRepo<User>, IUserRepo
{
    public UserRepo(ECommerceContext context) : base(context)
    {
    }
}
