using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL;

public class DashboardUserRepo:GenericRepo<User>,IDashboardUserRepo
{
    private readonly ECommerceContext _context;

    public DashboardUserRepo(ECommerceContext context):base(context)
    {
        _context = context;
    }

    public User GetByUserId(string userId)
    {
        return _context.Set<User>().Find(userId)!;
    }
}
