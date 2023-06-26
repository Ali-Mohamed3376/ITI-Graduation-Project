namespace Final.Project.DAL;
public interface IUserRepo
{
    User? GetById(string id);

    void Update(User user);
    void Delete(User user);
    Order GetUsersOrder(string id);
    IEnumerable<OrderProductDetails> GetUsersOrderDetails(int id);
    int savechanges();
}
