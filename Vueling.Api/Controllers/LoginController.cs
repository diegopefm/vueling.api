using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vueling.Api.Models;
using Vueling.Data;
using Vueling.Data.Models;

namespace Vueling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IOptions<AppSettings> settings;
        private readonly UserRepository repository;
        //private Context context;

        public LoginController(IOptions<AppSettings> settings, UserRepository repository) //Context context
        {
            this.settings = settings;
            this.repository = repository;
            //this.context = context;
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
            if (!validCredentials(login)) return userUnauthorized();

            TokenGenerator.settings = settings;
            var token = "{" + TokenGenerator.GenerateTokenJwt(login.Username) + "}";
            return new JsonResult(token);
        }

        private bool validCredentials(LoginRequest login)
        {

            if (login == null) return false;

            var user = repository.getUser(login.Username);
            if (login == null || user == null || user.Password != login.Password) return false;

            return true;
        }

        private ObjectResult userUnauthorized()
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid credentials!");
        }
    }
}