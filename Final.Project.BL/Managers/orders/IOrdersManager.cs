using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public interface IOrdersManager
{
    public void AddNewOrder(string userId, int addressId);

}
