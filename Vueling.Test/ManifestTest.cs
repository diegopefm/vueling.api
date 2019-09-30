using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Vueling.Data;
using Vueling.Data.Models;
using static Vueling.Data.Models.Response;

namespace Vueling.Test
{
    [TestClass]
    public class ManifestTest
    {
        [TestMethod]
        public void Get()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "Vueling")
                .Options;
            var context = new Context(options);
            PassengerRepository pr = new PassengerRepository(context);
            var passengers = pr.getPassengers("VL1234");
            Assert.AreEqual(passengers.GetType(), typeof(List<Passenger>));
        }

        [TestMethod]
        public void Add()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "Vueling")
                .Options;
            var context = new Context(options);
            PassengerRepository pr = new PassengerRepository(context);
            Passenger passenger = new Passenger(1, "TestName", "TestSurname", "A01", "VL1234");
            var response = pr.addPassenger(passenger);

            Assert.AreEqual(1, context.Passengers.ToList().Count());
            Assert.AreEqual("TestName", context.Passengers.ToList().First().Name);
            //optional
            Assert.AreEqual(response.Status, ResponseStatus.OK);
        }
    }
}
