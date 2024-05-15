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
            => Ok(await _userService.Authenticate(request));

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken token)
            => Ok(await _userService.Register(request, token));

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            _userService.Logout();
            return Ok();
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
            => Ok(await _userService.Refresh());
    }

    public class UserController(IUserService userWriteService) : BaseController
    {
        private readonly IUserService _userService = userWriteService;
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _userService.Profile());
    }
    public class ProductsController(ApplicationDbContext context) : BaseController
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product> products = await _context.Products.ToListAsync();
            System.Console.WriteLine(products);
            return Ok(products);
        }
    }
}
