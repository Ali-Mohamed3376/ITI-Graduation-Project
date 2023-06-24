namespace Final.Project.DAL;
public interface IUserAddressRepo : IGenericRepo<UserAddress>
{
    IEnumerable<UserAddress> GetAllUserAddresses(string userIdFromToken);
    void ResetDefaultAddress(string userIdFromToken);
}
