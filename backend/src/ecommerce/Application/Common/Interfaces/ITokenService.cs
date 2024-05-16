using System.Security.Claims;

namespace ecommerce.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public ClaimsPrincipal ValidateToken(string token);
    }
}