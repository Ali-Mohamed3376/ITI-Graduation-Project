using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        #region getuser
        [HttpGet]
        [Authorize]
        [Route("profile")]
        public ActionResult<UserReadDto> getUser()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            UserReadDto? user = _UsersManager.GetUserReadDto(currentUser.Id);

            if (user is null)
            {
                return NotFound();
            }


            return Ok(user);
        }
        #endregion

        [HttpPut]
        [Authorize]
        [Route("Update")]
        public ActionResult Edit(UserUpdateDto updateDto)
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            updateDto.Id = currentUser.Id;
            if (currentUser.Id is null)
            {
                return BadRequest("no content");
            }

            _UsersManager.Edit(updateDto);

            return NoContent();
        }

        #region Delete
        [HttpDelete]
        [Authorize]
        [Route("DeleteUser")]
        public ActionResult Delete()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;

            //id = currentUser.Id;

            var isfound = _UsersManager.delete(currentUser.Id);

            if (!isfound) { return NotFound(); }

            return NoContent();
        }
        #endregion

        [HttpPost]
        [Authorize]
        [Route("Change_Password")]
        public ActionResult ChangePassword(UserChangepassDto passwordDto)
        {
            //get the user
            User? currentUser = _Usermanager.GetUserAsync(User).Result;

            //confirm old password
            var isValiduser = _Usermanager.CheckPasswordAsync(currentUser!, passwordDto.OldPassword).Result;
            if (!isValiduser)
            {
                return BadRequest("Incorrect Password!!!");
            }
            //change password
             _Usermanager.ChangePasswordAsync(currentUser!,passwordDto.OldPassword,passwordDto.NewPassword);

                return Ok("Password Changed Successfully!!!");
        }
        #endregion

        #region user orders
        [HttpGet]
        [Authorize]
        [Route("orders")]
        public ActionResult<UserOrderDto> GetUserOrder()
        {

            var currentUser = _Usermanager.GetUserAsync(User).Result;

            UserOrderDto? userOrder = (UserOrderDto?)_UsersManager.GetUserOrderDto(currentUser.Id);

            if (userOrder is null)

            { return null; }

            return userOrder;

        }
        //[HttpGet]
        //[Route("{id}/Products")]

        //public ActionResult Userorder(int id)
        //{
        //    IEnumerable<UserProductDto>? Userpro = (IEnumerable<UserProductDto>?)_UsersManager.UserOrders();
        //    if (Userpro == null) { return NotFound(); }
        //    return Ok(Userpro);
        //}
        #endregion

        #region  user order details

        #endregion region 
        [HttpGet]
        [Authorize]
        [Route("order Details")]
        public ActionResult<UserOrderDetailsDto> getOrderDetails()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            var order = _UsersManager.GetUserOrderDto(currentUser.Id);
            UserOrderDetailsDto? userO = _UsersManager.GetUserOrderDetailsDto(order.Id);
            if (userO is null)
            {
                return NotFound();
            }

            return userO; //200 OK
        }
    }
}


