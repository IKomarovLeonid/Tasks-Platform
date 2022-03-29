using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.API.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            if (string.Equals(context.Username, _configuration.Administration.Username, StringComparison.Ordinal) &&
                string.Equals(context.Password, _configuration.Administration.Password, StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }

        public string GenerateToken(AuthenticationContext context)
        {
            var handler = new JwtSecurityTokenHandler()
            {
                InboundClaimTypeMap = new Dictionary<string, string>(),
                OutboundClaimTypeMap = new Dictionary<string, string>()
            };

            var jwt = _configuration.Authentication;
            var key = new SymmetricSecurityKey(Base64UrlTextEncoder.Decode(jwt.SigningKey));
            var signing = new List<SigningCredentials>().AddKey(key);
            var header = new JwtHeader(signing.FirstOrDefault());

            var now = DateTimeOffset.UtcNow;
            var props = new AuthenticationProperties
            {
                IssuedUtc = now,
                ExpiresUtc = now.AddHours(jwt.TokenExpirationHours)
            };

            var payload = new JwtPayload(jwt.Issuer, "", new Claim[]{new Claim(context.Username, context.Username)}, props.IssuedUtc?.UtcDateTime, props.ExpiresUtc?.UtcDateTime);
            var token = new JwtSecurityToken(header, payload);

            return handler.WriteToken(token);
        }
    }
}
