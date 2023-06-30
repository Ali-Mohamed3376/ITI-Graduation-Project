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

        #region profile

            #region getuser

            [HttpGet]
            [Route("profile")]
            public ActionResult<UserReadDto> getUserProfile()
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

            #region EditUserName

            [HttpPut]
            [Route("Edit")]
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

            #region Delete
            [HttpDelete]
            [Route("DeleteUser")]
            public ActionResult Delete()
            {
                var currentUser = _Usermanager.GetUserAsync(User).Result;

                //id = currentUser.Id;

                var isfound = _UsersManager.delete(currentUser.Id);

                if (!isfound) { return NotFound(); }

                return Ok("Deleted  Successfully");
            }
        #endregion
        //done
            #region ChangePassword

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

        #endregion


        //done
        #region GetUserOrders
        [HttpGet]
        [Route("orders")]
        public ActionResult<List<UserOrderDto>> GetUserOrders()
        {

            var currentUser = _Usermanager.GetUserAsync(User).Result;

            List<UserOrderDto>? userOrder = _UsersManager.GetUserOrdersDto(currentUser.Id) as List<UserOrderDto>;

            if (userOrder is null)

            { return null; }

            return Ok(userOrder);

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

        //done
        #region getUserOrderDetails
        [HttpGet]
        [Route("orderDetails/{orderId}")]
        public ActionResult<UserOrderDetailsDto> GetOrderDetails(int orderId)
        {
            //var currentUser = _Usermanager.GetUserAsync(User).Result;
           // var order = _UsersManager.GetUserOrderDto(currentUser.Id);
            var products = _UsersManager.GetUserOrderDetailsDto(orderId);
            if (products is null)
            {
                return NotFound();
            }

            return Ok(products); //200 OK
        }
        #endregion region 
    }
}


