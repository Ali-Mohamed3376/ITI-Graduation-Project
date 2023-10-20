using Final.Project.API.Responses;
using Final.Project.BL;

namespace Final.Project.API;
public interface IUserService
{
    Task<UserManagerResponse> Login(LoginDto loginCredientials);

}
