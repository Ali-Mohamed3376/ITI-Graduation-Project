using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
            private readonly IUsersManager? _UsersManager ;
            private readonly UserManager<User> _Usermanager;


        public UserProfileController(IUsersManager userManager, UserManager<User> manager)
            {
                _UsersManager = userManager;
                _Usermanager= manager;
            }
            //get with id
            [HttpGet]
           [Authorize]
          public ActionResult<UserReadDto> getUser(string id)
            {
                var currentUser=_Usermanager.GetUserAsync(User).Result;
                id = currentUser.Id;
                UserReadDto? user = _UsersManager.GetUserReadDto(id);
                if (user is null)
                {
                    return NotFound();
                }

                return user;
            }


            [HttpPut]
            [Authorize]
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

        //  [Route("{id}")]
        public ActionResult Delete(string id)
            {
                var currentUser = _Usermanager.GetUserAsync(User).Result;
                id = currentUser.Id;
                var isfound = _UsersManager.delete(id);
                if (!isfound) { return NotFound(); }
                return NoContent();
            }


        
    }
}
