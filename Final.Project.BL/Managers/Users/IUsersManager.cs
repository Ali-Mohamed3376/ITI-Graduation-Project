using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public interface IUsersManager
{
    UserReadDto GetUserReadDto(string id);
    bool Edit(UserUpdateDto updateDto);
    bool delete(string id);
    UserOrderDto GetUserOrderDto(string id);
}
