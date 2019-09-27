using System.Collections.Generic;
using System.Linq;
using Vueling.Data.Models;

namespace Vueling.Data
{
    public class CustomerRepository
    {
        public Context context;

        public CustomerRepository(Context context) {
            this.context = context;
        }

        //public List<User> getUsers() {
            
        //    return context.Users.ToList();
        //}

        //public User getUser(string user) {

        //    return context.Users.Where(x => x.Login == user).FirstOrDefault();
        //}
    }
}
