namespace Vueling.Api.Models
{
    public class AppSettings
    {
        public string jwt_secret_key { get; set; }
        public string jwt_audience_token { get; set; }
        public string jwt_issuer_token { get; set; }
        public string jwt_expire_minutes { get; set; }
        public string database_connection { get; set; }
        public string crypto_digit32 { get; set; }
        public string crypto_digit16 { get; set; }
    }
}
