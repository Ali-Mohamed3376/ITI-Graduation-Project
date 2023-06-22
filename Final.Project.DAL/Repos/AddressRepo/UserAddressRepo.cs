namespace Final.Project.DAL;
public class UserAddressRepo : GenericRepo<UserAddress>, IUserAddressRepo
{
    public UserAddressRepo(ECommerceContext context) : base(context)
    {
    }
}
