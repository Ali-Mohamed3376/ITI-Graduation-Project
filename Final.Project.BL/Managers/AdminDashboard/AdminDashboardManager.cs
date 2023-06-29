using Final.Project.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class AdminDashboardManager : IAdminDashboardManager
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminDashboardManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public bool DeleteUser(string userId)
    {
        User? user = _unitOfWork.UserRepo.GetById(userId);

        if (user is null) { return false; }

        _unitOfWork.UserRepo.Delete(user);
        _unitOfWork.Savechanges();
        return true;
    }

    public IEnumerable<UserDashboardReadDto> GetAllUsers()
    {
        IEnumerable<User> UsersFromDB = _unitOfWork.DashboardUserRepo.GetAll();
        IEnumerable<UserDashboardReadDto> AllUsersDetails = UsersFromDB.Select(u => new UserDashboardReadDto
        {
            Id = u.Id,
            City = u.City,
            Street = u.Street,
            FName = u.FName,
            LName = u.LName,
            Email = u.Email,
            Role = u.Role.ToString()
        });
        return AllUsersDetails;
    }

    public UserDashboardReadDto GetUserById(string userId)
    {
        User userFromDB = _unitOfWork.DashboardUserRepo.GetByUserId(userId);
        UserDashboardReadDto user = new UserDashboardReadDto
        {
            Id = userFromDB.Id,
            City = userFromDB.City,
            Street = userFromDB.Street,
            Email = userFromDB.Email,
            FName = userFromDB.FName,
            LName = userFromDB.LName,
            Role=userFromDB.Role.ToString(),
        };
        return user;
    }

   
}
