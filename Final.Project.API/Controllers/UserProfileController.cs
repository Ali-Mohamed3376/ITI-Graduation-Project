using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUsersManager? _UsersManager;
        private readonly UserManager<User> _Usermanager;


        public UserProfileController(IUsersManager userManager, UserManager<User> manager)
        {
            _UsersManager = userManager;
            _Usermanager = manager;
        }

        #region profile
        [HttpGet]
        [Authorize]
        public ActionResult<UserReadDto> getUser()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            //id = currentUser.Id;
            UserReadDto? user = _UsersManager.GetUserReadDto(currentUser.Id);

            if (user is null)
            {
                return NotFound();
            }


            return user;
        }


        [HttpPut]
        [Authorize]
        [Route("Update")]
        public ActionResult Edit(UserUpdateDto updateDto)
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;

            updateDto.Id = currentUser.Id;

            var isfound = _UsersManager.Edit(updateDto);

            if (!isfound) { return NotFound(); }

            return NoContent();

        }

        [HttpDelete]
        [Authorize]

        public ActionResult Delete()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;

            //id = currentUser.Id;

            var isfound = _UsersManager.delete(currentUser.Id);

            if (!isfound) { return NotFound(); }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        [Route("Change_Password")]
        public ActionResult ChangePassword(UserChangepassDto passwordDto)
        {
            //get the user
            User? currentUser = _Usermanager.GetUserAsync(User).Result;

            //confirm old password
            bool isValiduser = _Usermanager.CheckPasswordAsync(currentUser, passwordDto.Oldpassword).Result;
            if (!isValiduser)
            {
                return BadRequest("Incorrect Password!!!");
            }
            //change password
            var newP= _Usermanager.ChangePasswordAsync(currentUser,passwordDto.Oldpassword,passwordDto.Newpassword).Result;

            if (!newP.Succeeded)
            {
                return BadRequest(newP.Errors);
            }

            return Ok("Password Changed Successfully!!!");
        }
        #endregion

        #region user orders
        [HttpGet]
        [Authorize]
        [Route("orders")]
        public ActionResult <UserOrderDto> GetUserOrder() {

            var currentUser = _Usermanager.GetUserAsync(User).Result;

           // id = currentUser.Id;
            UserOrderDto? userOrder= (UserOrderDto?)_UsersManager.GetUserOrderDto(currentUser.Id);

            if (userOrder is null)

            { return null; }

            return userOrder;

        }
        #endregion

        #region  user order details

        #endregion region 



    }

}
