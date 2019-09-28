using System;
using System.Collections.Generic;
using System.Linq;
using Vueling.Data.Models;
using static Vueling.Data.Models.Response;

namespace Vueling.Data
{
    public class PassengerRepository
    {
        public Context context;

        public PassengerRepository(Context context) {
            this.context = context;
        }

        /// <summary>
        /// Returns a list of passengers of flight <flight>
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public List<Passenger> getPassengers(string flight)
        {
            return context.Passengers.ToList().Where(x => x.Flight == flight).ToList();
        }

        /// <summary>
        /// Adds a passenger to database
        /// </summary>
        /// <param name="passenger"></param>
        public Response addPassenger(Passenger passenger)
        {
            string result = ResponseStatus.OK;
            string message = "Passenger was added correctly";
            try
            {
                //check if seats is already taken
                var seat = context.Passengers.Where(x => x.Seat == passenger.Seat).FirstOrDefault();
                if (seat != null) return new Response { Status = ResponseStatus.KO, Message = "Seat already taken, please choose another one." };
                context.Passengers.Add(passenger);
                context.SaveChanges();
            }
            catch (Exception)
            {
                result = ResponseStatus.KO;
                message = "There was a problem adding passenger";
            }
            return new Response { Status = result, Message = message };
        }
    }
}
