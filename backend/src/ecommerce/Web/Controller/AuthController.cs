using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Web.Controller
{
    public class AuthController(IUserService userWriteService) : BaseController
    {
        private readonly IUserService _userWriteService = userWriteService;

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginRequest request)
            => Ok(await _userWriteService.Authenticate(request));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken token)
            => Ok(await _userWriteService.Register(request, token));

    }
}
