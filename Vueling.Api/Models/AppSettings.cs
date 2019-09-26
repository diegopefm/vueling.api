using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vueling.Api.Models
{
    public class AppSettings
    {
        public string jwt_secret_key { get; set; }
        public string jwt_audience_token { get; set; }
        public string jwt_issuer_token { get; set; }
        public string jwt_expire_minutes { get; set; }
    }
}
