using System;
using Microsoft.AspNetCore.WebUtilities;

namespace Core.API.Configuration
{
    public class ApplicationConfiguration
    {
        public string Url { get; set; } = "http://localhost:8080";

        public AuthenticationSettings Authentication { get; set; } = new AuthenticationSettings();

        public AdminSettings Administration { get; set; } = new AdminSettings();
    }

    public class AdminSettings
    {
        public string Username { get; set; } = "admin";

        public string Password { get; set; } = "password";
    }

    public class AuthenticationSettings
    {
        public bool AllowInsecure { get; set; } = true;
        public string TokenEndpoint { get; set; } = "/api/oauth2/token";
        public int TokenExpirationHours { get; set; } = 2;
        public string Issuer { get; set; }
        public string SigningKey { get; set; } = GenerateRandomSigningKey();
        public string GenerationKey { get; set; } = GenerateRandomSigningKey();

        //helpers
        private static string GenerateRandomSigningKey()
        {
            var rand = new Random();
            var bytes = new byte[32];
            rand.NextBytes(bytes);
            return Base64UrlTextEncoder.Encode(bytes);
        }
    }
}
