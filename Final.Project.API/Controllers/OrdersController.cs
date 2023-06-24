using Final.Project.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersManager _ordersManager;
        
        public OrdersController(IOrdersManager ordersManager)
        {
            _ordersManager = ordersManager;
        }
        [HttpGet]
        public ActionResult MakeNewOrder()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            _ordersManager.AddNewOrder(userIdFromToken);


            return Ok("order Added Successfully");
        }
    }
}
