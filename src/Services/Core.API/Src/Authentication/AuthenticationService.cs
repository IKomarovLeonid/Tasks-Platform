using System;
using Core.API.Configuration;

namespace Core.API.Authentication
{
    internal class AuthenticationService : IAuthentication
    {
        private readonly ApplicationConfiguration _configuration;

        public AuthenticationService(ApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsAuthenticated(AuthenticationContext context)
        {
            if (string.Equals(context.Username, _configuration.Admin, StringComparison.Ordinal) &&
                string.Equals(context.Password, _configuration.Password, StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }
    }
}
