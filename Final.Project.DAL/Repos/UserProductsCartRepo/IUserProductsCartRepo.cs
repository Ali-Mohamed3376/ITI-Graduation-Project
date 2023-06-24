namespace Final.Project.DAL;
public interface IUserProductsCartRepo : IGenericRepo<UserProductsCart>
{
    IEnumerable<UserProductsCart> GetAllProductsByUserId(string userId);
    UserProductsCart GetByCompositeId(int ProductID, string userID);
}
