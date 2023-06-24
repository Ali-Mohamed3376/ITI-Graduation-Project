using Final.Project.Bl;
using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressesController : ControllerBase
    {
        private readonly IUserAddressesManager _userAddressesManager;

        public UserAddressesController(IUserAddressesManager userAddressesManager)
        {
            _userAddressesManager = userAddressesManager;
        }

        [HttpGet]
        public ActionResult GetAllUserAddresses()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            var addresses = _userAddressesManager.GetAllUserAddresses(userIdFromToken);

            return Ok(addresses);
        }



        [HttpPost]
        public ActionResult AddNewAddress(NewAddressAddingDto newAddress)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            _userAddressesManager.AddNewAddress(userIdFromToken,newAddress);

            return Ok("Address Added Successfully");

        }

        [HttpPut]
        [Route("Edit")]

        public ActionResult EditAddressDetails(AddressEditDto address)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            _userAddressesManager.EditAddress(userIdFromToken, address);

            return Ok("address Edited successfully");
        }

        [HttpPut]
        [Route("SetDefault/{AddressId}")]
        public ActionResult SetDefaultAddress(int AddressId)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            _userAddressesManager.SetDefaultAddress(userIdFromToken,AddressId);
            return Ok("Set as default succeeded");
        }


        [HttpDelete]
        [Route("delete/{addressId}")]
        public ActionResult DeleteAddress(int addressId)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            _userAddressesManager.Delete(addressId);

            return Ok("Address deleted Successfully");
        }




    }
}
