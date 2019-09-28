using System.Collections.Generic;
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
    //[Authorize]
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

        [HttpGet("{flight}")]
        [EnableCors("VuelingPolicy")]
        public ActionResult Get(string flight)
        {
            var passengers = repository.getPassengers(flight);
            return new JsonResult(passengers);
        }

        [HttpPost]
        [Route("add")]
        [EnableCors("VuelingPolicy")]
        public ActionResult Add(Passenger passenger)
        {
            string result = repository.addPassenger(passenger) == "OK" ? "Passenger was added correctly" : "There was a problem adding passenger";
            return new JsonResult(result);
        }

        private ObjectResult userUnauthorized()
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid credentials!");
        }
    }
}