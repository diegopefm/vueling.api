namespace Vueling.Data.Models
{
    public partial class Response
    {
        public struct ResponseStatus
        {
            public const string OK = "OK";
            public const string KO = "KO";
        };

        public string Status { get; set; }
        public string Message { get; set; }
    }
}
