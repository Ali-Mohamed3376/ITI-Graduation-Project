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
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUsersManager? _UsersManager;
        private readonly UserManager<User> _Usermanager;


        public UserProfileController(IUsersManager userManager, UserManager<User> manager)
        {
            _UsersManager = userManager;
            _Usermanager = manager;
        }

        #region User Profile

        #region Get user

        
        [HttpGet]
        [Route("profile")]
        public ActionResult<UserReadDto> GetUser()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            //UserReadDto? user = _UsersManager.GetUserReadDto(currentUser.Id);
            UserReadDto? user = _UsersManager.GetUserReadDto(currentUser);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        #endregion

        #region Update User

        
        [HttpPut]
        [Route("Update")]
        public ActionResult Edit(UserUpdateDto updateDto)
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            //updateDto.Id=currentUser.Id;
            if (currentUser.Id is null)
            {
                return BadRequest("Edit failed");
            }

            // _UsersManager.Edit(updateDto,currentUser.Id);
            _UsersManager.Edit(updateDto, currentUser);


            return Ok("Edit successfully");
        }

        #endregion

        #region Delete User
        [HttpDelete]
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

        #region Change Password

        [HttpPost]
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
            var test = _Usermanager.ChangePasswordAsync(currentUser!, passwordDto.OldPassword, passwordDto.NewPassword).Result;

            return Ok("Password Changed Successfully!!!");
        }


        #endregion
        
        #region user orders
        
        [HttpGet]
        [Route("Orders")]
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

        #region Order Details
        
        [HttpGet]
        [Route("Order Details/{orderId}")]
        public ActionResult<UserOrderDetailsDto> getOrderDetails(int orderId)
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            if (currentUser is null)
            {
                return NotFound();
            }

            // var order = _UsersManager.GetUserOrderDto(currentUser.Id);
            var products = _UsersManager.GetUserOrderDetailsDto(orderId);
            if (products is null)
            {
                return NotFound();
            }

            return Ok(products); //200 OK
        }
        #endregion

        #endregion
    }
}


