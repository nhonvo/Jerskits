using System.Security.Claims;
using AutoMapper;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models.Response;
using ecommerce.Application.Common.Models.User;
using ecommerce.Application.Common.Utilities;
using ecommerce.Infrastructure.Interface;

namespace ecommerce.Application.Services;

public class UserService(IUnitOfWork unitOfWork,
                         IMapper mapper,
                         IHttpContextAccessor httpContextAccessor,
                         IUserRepository userRepository,
                         ICurrentUser currentUser,
                         ITokenService tokenService) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<ResultResponse<SignInResponse>> SignIn(LoginRequest request)
    {

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

        var token = _tokenService.GenerateToken(user);
        // Set cookies
        _httpContextAccessor.HttpContext.Response.Cookies.Append("acc", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None, // Or SameSiteMode.Lax if not using HTTPS
            Secure = true, // Set to true if using HTTPS
            MaxAge = TimeSpan.FromMinutes(30)
        });
        var response = new ResultResponse<SignInResponse>
        {
            error = false,
            message = "Welcome back!",
            data = _mapper.Map<SignInResponse>(user)
        };

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

    public async Task<ResultResponse<UserProfileResponse>> GetProfile()
    {
        try
        {
            var jwtCookie = _httpContextAccessor.HttpContext.Request.Cookies["acc"];
            if (string.IsNullOrEmpty(jwtCookie))
            {
                throw new UserFriendlyException(ErrorCode.Unauthorized, "user not logged in");
            }
            var token = _tokenService.ValidateToken(jwtCookie);
            var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userRepository.GetByIdAsync(int.Parse(userId));

            var result = new ResultResponse<UserProfileResponse>
            {
                error = false,
                data = _mapper.Map<UserProfileResponse>(user)
            };
            return result;
        }
        catch (Exception exception)
        {
            throw new UserFriendlyException(ErrorCode.Internal, "something went wrong", exception);
        }
    }

    public async Task<string> RefreshToken()
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(_currentUser.GetCurrentUserId());

            var accessToken = _tokenService.GenerateToken(user);

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

    public async Task<UserDTO> SignUp(RegisterRequest request, CancellationToken token)
    {
        var isUserExist = await _unitOfWork.UserRepository.AnyAsync(x => x.Email == request.Email);
        if (isUserExist)
            throw new UserFriendlyException(ErrorCode.BadRequest, "This Email Already Used!");

        var user = _mapper.Map<User>(request);
        user.Password = user.Password.Hash();
        await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.UserRepository.AddAsync(user), token);

        var response = _mapper.Map<UserDTO>(user);

        return response;
    }

    public async Task Update(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        string jwtCookie;
        try
        {
            jwtCookie = _httpContextAccessor.HttpContext.Request.Cookies["acc"];
        }
        catch (Exception exception)
        {
            throw new UserFriendlyException(ErrorCode.Unauthorized, "user not logged in", $"Access token not found in cookies{exception.Message}");
        }

        var token = _tokenService.ValidateToken(jwtCookie);

        var userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var user = await _userRepository.GetByIdAsync(int.Parse(userId));
        _mapper.Map(request, user);

        await _unitOfWork.ExecuteTransactionAsync(() =>
            _unitOfWork.UserRepository.Update(user), cancellationToken);
    }
}
