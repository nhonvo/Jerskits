using System.Security.Claims;
using ecommerce.Application.Common.Interfaces;

namespace ecommerce.Web.Services;

public class CurrentUser : IUser
{
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        UserId = string.IsNullOrEmpty(Id) ? 0 : int.Parse(Id);

    }
    public int? UserId { get; }
}
