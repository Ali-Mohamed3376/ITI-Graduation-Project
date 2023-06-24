namespace Final.Project.DAL;
public interface IUserRepo
{
    Order GetUsersOrder(string id);
    User? GetById(string id);
    void Update(User user);
    void Delete(User user);
    int savechanges();
}
