namespace Final.Project.DAL;
public interface IUserRepo
{
    User? GetById(string id);
    void Update(User user);
    void Delete(User user);
    int savechanges();
}
