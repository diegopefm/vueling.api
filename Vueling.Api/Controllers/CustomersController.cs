using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vueling.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetId(int id)
        {
            var customerFake = "customer-fake";
            return Ok(customerFake);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok(customersFake);
        }
    }
}