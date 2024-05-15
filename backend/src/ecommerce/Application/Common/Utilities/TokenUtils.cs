using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ecommerce.Application.Common.Interfaces;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce.Application.Common.Utilities
{
    public static class TokenUtils
    {
        public static string Authenticate(this User user, string issuer, string audience, string key, ICurrentTime time)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: time.GetCurrentTime().AddMinutes(10),
                    audience: audience,
                    issuer: issuer,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // For MVC
        public static ClaimsPrincipal Validate(this string token, string issuer, string audience, string key)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new()
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

            return principal;
        }

        public static DateTime GetExpireDate(this string token, ICurrentTime currentTime)
        {
            JwtSecurityToken jwt = new(token);
            return string.IsNullOrEmpty(token) is false ? currentTime.GetCurrentTime() : jwt.ValidTo.ToUniversalTime();
        }
    }
}
