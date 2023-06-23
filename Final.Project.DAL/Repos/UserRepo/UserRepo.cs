using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace Final.Project.DAL;
public class UserRepo : IUserRepo
{
    private readonly ECommerceContext _context;
    public UserRepo(ECommerceContext context)
    {
        _context = context;

    }
    public User? GetById(string id)
    {
        return _context.Set<User>().Find(id);
    }

    public void Delete(User user)
    {
        _context.Set<User>().Remove(user);
    }



    public void Update(User user)
    {
        //
    }

    public int savechanges()
    {
        return _context.SaveChanges();
    }
}