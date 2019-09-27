using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vueling.Api.Models;
using Vueling.Data;

namespace Vueling.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManifestController : ControllerBase
    {
        private readonly IOptions<AppSettings> settings;
        private readonly PassengerRepository repository;

        public ManifestController(IOptions<AppSettings> settings, PassengerRepository repository)
        {
            this.settings = settings;
            this.repository = repository;
        }

        [HttpPost]
        [Route("getmanifest")]
        [EnableCors("VuelingPolicy")]
        public ActionResult GetManifest(string flight)
        {
            var passengers = repository.getPassengers(flight);
            return Ok(passengers);
        }

        private ObjectResult userUnauthorized()
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid credentials!");
        }
    }
}