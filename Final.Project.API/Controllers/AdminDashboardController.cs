using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardManager _adminDashboardManager;
        private readonly UserManager<User> _manager;

        public AdminDashboardController(IAdminDashboardManager adminDashboardManager,
                                          UserManager<User> manager)
        {
            
            _adminDashboardManager = adminDashboardManager;
            _manager = manager;
        }
        [HttpGet]
        [Route("AllUsers")]
        public ActionResult GetAllUsers()
        {
            IEnumerable<UserDashboardReadDto> allUsers= _adminDashboardManager.GetAllUsers();
            return Ok(allUsers);
        }

        [HttpGet]
        [Route("User/{userId}")]
        public ActionResult GetUserById(string userId)
        {
            UserDashboardReadDto user = _adminDashboardManager.GetUserById(userId);
            return Ok(user);
        }

        

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterAtDashboardDto credentials)
        {

            User user = new User
            {
                FName = credentials.FName,
                LName = credentials.LName,
                UserName = credentials.Email,
                Email = credentials.Email,
                Role = (Role)Enum.Parse(typeof(Role),credentials.Role)
            };

            var result = _manager.CreateAsync(user, credentials.Password).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Role",user.Role.ToString()),
            };

            var claimsResult = _manager.AddClaimsAsync(user, claims).Result;

            if (!claimsResult.Succeeded)
            {
                return BadRequest(claimsResult.Errors);
            }

            return Ok("Register Succeded!!!");
        }

        [HttpDelete]
        [Route("userDelete/{userId}")]
        public ActionResult DeleteUserFromDashboard(string userId)
        {
            _adminDashboardManager.DeleteUser(userId);

            return Ok("user deleted Successfully");

        }
    }
}
