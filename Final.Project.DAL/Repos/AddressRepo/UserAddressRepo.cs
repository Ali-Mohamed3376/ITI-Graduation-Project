namespace Final.Project.DAL;
public class UserAddressRepo : GenericRepo<UserAddress>
{
    public UserAddressRepo(ECommerceContext context) : base(context)
    {
    }
}
