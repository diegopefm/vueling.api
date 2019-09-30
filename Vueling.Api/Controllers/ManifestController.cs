using System.Net;
using Microsoft.AspNetCore.Authorization;
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
    [EnableCors("Cors")]
    public class ManifestController : ControllerBase
    {
        private readonly IOptions<AppSettings> settings;
        private readonly PassengerRepository repository;

        public ManifestController(IOptions<AppSettings> settings, PassengerRepository repository)
        {
            this.settings = settings;
            this.repository = repository;
        }

        [HttpGet("{flight}")]
        public ActionResult Get(string flight)
        {
            var passengers = repository.getPassengers(flight);
            return new JsonResult(passengers);
        }

        [HttpPost]
        [Route("add")]
        [Authorize]
        public ActionResult Add(Passenger passenger)
        {
            Response response = repository.addPassenger(passenger);
            return new JsonResult(response);
        }

        private ObjectResult userUnauthorized()
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid credentials!");
        }
    }
}