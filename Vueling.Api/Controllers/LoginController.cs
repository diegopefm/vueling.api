using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vueling.Api.Models;

namespace Vueling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<AppSettings> settings;

        public LoginController(IOptions<AppSettings> settings)
        {
            this.settings = settings;
        }

        [HttpGet]
        [Route("echoping")]
        public ActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public ActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        [EnableCors("VuelingPolicy")]
        public ActionResult Authenticate(LoginRequest login)
        {
            if (login == null) return Unauthorized();

            //TODO: Validate credentials Correctly.
            bool isCredentialValid = (login.Password == "123456");
            if (isCredentialValid)
            {
                TokenGenerator.settings = settings;
                var token = "{" + TokenGenerator.GenerateTokenJwt(login.Username) + "}";
                return new JsonResult(token);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid credentials!");
            }
        }
    }
}