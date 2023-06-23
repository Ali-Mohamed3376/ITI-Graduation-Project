using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class UsersManager : IUsersManager
{
    private readonly IUserRepo? _userRepo;


    public UsersManager(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public bool delete(string id)
    {
        User? user = _userRepo.GetById(id);
        if (user is null) { return false; }
        _userRepo.Delete(user);
        _userRepo.savechanges();
        return true;
    }

    public bool Edit(UserUpdateDto updateDto)
    {
        User? user = _userRepo.GetById(updateDto.Id);
        if (user is null) { return false; };
        user.FName = updateDto.Fname;
        user.LName = updateDto.Lname;
        user.Street = updateDto.Street;
        user.City = updateDto.City;
        _userRepo.Update(user);
        _userRepo.savechanges();
        return true;

    }

    public UserReadDto GetUserReadDto(string id)
    {
        User? user = _userRepo.GetById(id);
        if (user == null)
        {
            return null;
        }
        return new UserReadDto
        {
            Fname = user.FName,
            Lname = user.LName,
            City = user.City,
            Street = user.Street,
            
        };

    }
}
