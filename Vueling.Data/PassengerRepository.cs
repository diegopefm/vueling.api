using System;
using System.Collections.Generic;
using System.Linq;
using Vueling.Data.Models;

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
        public string addPassenger(Passenger passenger)
        {
            string result = "OK";
            try
            {
                context.Passengers.Add(passenger);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                result = "KO";
            }            
            return result;
        }
    }
}
