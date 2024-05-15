using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using ecommerce.Application.Common;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models.User;
using ecommerce.Application.Common.Utilities;
using ecommerce.Infrastructure.Interface;

namespace ecommerce.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             ICurrentTime currentTime,
                             AppSettings configuration,
                             ILoggerFactory logger,
                             IHttpContextAccessor httpContextAccessor,
                             IUserRepository userRepository) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentTime _currentTime = currentTime;
        private readonly AppSettings _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILogger _logger = logger.CreateLogger<UserService>();
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDTO> Authenticate(LoginRequest request)
        {
            _logger.LogInformation("Request: " + JsonSerializer.Serialize(request));

            var isUserExist = await _unitOfWork.UserRepository.AnyAsync(x => x.Email == request.Email);
            if (!isUserExist)
            {
                throw new UserFriendlyException(ErrorCode.BadRequest, "User does not exist!");
            }

            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (!StringHelper.Verify(request.Password, user.Password))
            {
                throw new UserFriendlyException(ErrorCode.BadRequest, "Password Incorrect!");
            }

            var token = user.Authenticate(
                _configuration.Jwt.Issuer,
                _configuration.Jwt.Audience,
                _configuration.Jwt.Key,
                _currentTime);
            var response = _mapper.Map<UserDTO>(user);
            response.Token = token;
            // Set cookies
            _httpContextAccessor.HttpContext.Response.Cookies.Append("acc", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None, // Or SameSiteMode.Lax if not using HTTPS
                Secure = true, // Set to true if using HTTPS
                MaxAge = TimeSpan.FromMinutes(30)
            });

            _logger.LogInformation("Response: " + JsonSerializer.Serialize(response));

            return response;
        }

        public void Logout()
        {
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;

            if (cookies.ContainsKey("acc"))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("acc");
            }

            if (cookies.ContainsKey("ref"))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("ref");
            }
        }

        public async Task<UserProfile> Profile()
        {
            try
            {
                var jwtCookie = _httpContextAccessor.HttpContext.Request.Cookies["acc"];
                if (string.IsNullOrEmpty(jwtCookie))
                {
                    throw new UserFriendlyException(ErrorCode.Unauthorized, "user not logged in");
                }
                var token = jwtCookie.Validate(_configuration.Jwt.Issuer,
                    _configuration.Jwt.Audience,
                    _configuration.Jwt.Key);
                var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var user = await _userRepository.GetByIdAsync(int.Parse(userId));

                var result = _mapper.Map<UserProfile>(user);
                return result;
            }
            catch (Exception exception)
            {
                throw new UserFriendlyException(ErrorCode.Internal, "something went wrong", exception);
            }
        }

        public async Task<string> Refresh()
        {
            try
            {
                var jwtCookie = _httpContextAccessor.HttpContext.Request.Cookies["acc"];
                if (string.IsNullOrEmpty(jwtCookie))
                {
                    throw new UserFriendlyException(ErrorCode.Unauthorized, "user not logged in");
                }
                var token = jwtCookie.Validate(_configuration.Jwt.Issuer,
                    _configuration.Jwt.Audience,
                    _configuration.Jwt.Key);
                var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var user = await _userRepository.GetByIdAsync(int.Parse(userId));

                var accessToken = user.Authenticate(
                                _configuration.Jwt.Issuer,
                                _configuration.Jwt.Audience,
                                _configuration.Jwt.Key,
                                _currentTime);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("ref", accessToken, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    MaxAge = TimeSpan.FromMinutes(30)
                });
                return accessToken;
            }
            catch (Exception exception)
            {
                throw new UserFriendlyException(ErrorCode.Internal, "something went wrong", exception);
            }
        }

        public async Task<UserDTO> Register(RegisterRequest request, CancellationToken token)
        {
            _logger.LogInformation("Request: " + JsonSerializer.Serialize(request));

            var isUserExist = await _unitOfWork.UserRepository.AnyAsync(x => x.Email == request.Email);
            if (isUserExist)
                throw new UserFriendlyException(ErrorCode.BadRequest, "This Email Already Used!");

            var isEmailExist = await _unitOfWork.UserRepository.AnyAsync(x => x.Email == request.Email);
            if (isEmailExist)
                throw new UserFriendlyException(ErrorCode.BadRequest, "This Email Already Used");


            var user = _mapper.Map<User>(request);
            user.Password = user.Password.Hash();
            await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.UserRepository.AddAsync(user), token);

            var response = _mapper.Map<UserDTO>(user);
            _logger.LogInformation("Response: " + JsonSerializer.Serialize(response));

            return response;
        }
    }
}
