using Final.Project.DAL;
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
    public IEnumerable<AllUsersReadDto> GetAllUsers()
    {
        IEnumerable<User> UsersFromDB = _unitOfWork.DashboardUserRepo.GetAll();
        IEnumerable<AllUsersReadDto> AllUsersDetails = UsersFromDB.Select(u => new AllUsersReadDto
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

    public UserReadDto GetUserById(string userId)
    {
        User userFromDB = _unitOfWork.DashboardUserRepo.GetByUserId(userId);
        UserReadDto user = new UserReadDto
        {
            Id = userFromDB.Id,
            City = userFromDB.City,
            Street = userFromDB.Street,
            Email = userFromDB.Email,
            Fname = userFromDB.FName,
            Lname = userFromDB.LName
        };
        return user;
    }
}
