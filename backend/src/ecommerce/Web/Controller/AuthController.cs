using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models.User;
using ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Web.Controller
{
    public class AuthController(IUserService userWriteService) : BaseController
    {
        private readonly IUserService _userService = userWriteService;

        [HttpPost("sign-in")]
        public async Task<IActionResult> Authenticate(LoginRequest request)
            => Ok(await _userService.SignIn(request));

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken token)
            => Ok(await _userService.SignUp(request, token));

        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            _userService.Logout();
            return Ok();
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
            => Ok(await _userService.RefreshToken());
    }

    public class UserController(IUserService userWriteService) : BaseController
    {
        private readonly IUserService _userService = userWriteService;
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _userService.GetProfile());
        [HttpPatch("/api/profile")]
        public async Task<IActionResult> Update(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            await _userService.Update(request, cancellationToken);
            return NoContent();
        }
        [HttpGet("/api/profile")]
        public async Task<IActionResult> Profile()
            => Ok(await _userService.GetProfile());
    }
    public class ProductsController(IProductService productService) : BaseController
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex, int? pageSize)
            => Ok(await _productService.Get(pageIndex, pageSize));

        [HttpGet("search")]
        public async Task<IActionResult> Search(string q)
            => Ok(await _productService.Search(q));
    }
}
