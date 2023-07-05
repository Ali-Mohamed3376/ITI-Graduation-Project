using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public interface IWishListManager
{
    bool AddtoWishList(string userIdFromToken, int productId);
    int GetWishListCount(string userIdFromToken);
}
