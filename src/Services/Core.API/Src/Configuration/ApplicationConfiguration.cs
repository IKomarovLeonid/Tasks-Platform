namespace Core.API.Configuration
{
    public class ApplicationConfiguration
    {
        public string Url { get; set; } = "http://localhost:8080";

        public string Admin { get; set; } = "admin";

        public string Password { get; set; } = "password";
    }
}
