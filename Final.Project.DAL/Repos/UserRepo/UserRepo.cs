using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

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
    //public User? GetByName(string name) 
    //{  return _context.Set<User>().Find(name); }

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
    #region user order
    public Order? GetUsersOrder(string id)
    {
        return (Order?)_context.Orders.Where(x => x.UserId == id); //Include(x => x.OrdersProductDetails).ThenInclude(x => x.Product)
           
    }
    #endregion
}