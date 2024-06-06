using AutoMapper;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models.Response;
using ecommerce.Application.Common.Models.User;
using ecommerce.Application.Common.Utilities;
using ecommerce.Domain.Constants;
using ecommerce.Infrastructure.Interface;

namespace ecommerce.Application.Services;
public class UserService(IUnitOfWork unitOfWork,
                         IMapper mapper,
                         ITokenService tokenService,
                         ICurrentUser currentUser,
                         IUserRepository userRepository,
                         ICookieService cookieService
                         ) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICookieService _cookieService = cookieService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResultResponse<SignInResponse>> SignIn(LoginRequest request)
    {
        var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == request.Email)
            ?? throw UserException.BadRequestException(UserErrorMessage.UserNotExist);

        if (!StringHelper.Verify(request.Password, user.Password))
        {
            throw UserException.BadRequestException(UserErrorMessage.PasswordIncorrect);
        }

        var token = _tokenService.GenerateToken(user);
        _cookieService.Set(token);

        var response = new ResultResponse<SignInResponse>
        {
            error = false,
            message = "Welcome back!",
            data = _mapper.Map<SignInResponse>(user)
        };
        return response;
    }

    public async Task<RegisterResponse> SignUp(RegisterRequest request, CancellationToken token)
    {
        var isEmailExist = await _unitOfWork.UserRepository.AnyAsync(x => x.Email == request.Email);
        if (isEmailExist)
            throw UserException.UserAlreadyExistsException(request.Email);

        var user = _mapper.Map<User>(request);
        user.Password = user.Password.Hash();
        await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.UserRepository.AddAsync(user), token);

        var response = new RegisterResponse
        {
            error = false,
            message = "thanks for sign up, now you can sign in!",
        };
        return response;
    }

    public void Logout()
    {
        try
        {
            _ = _cookieService.Get();
            _cookieService.Delete();
        }
        catch { }
    }

    public async Task<ResultResponse<UserProfileResponse>> GetProfile()
    {
        var userId = _currentUser.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        var result = new ResultResponse<UserProfileResponse>
        {
            error = false,
            data = _mapper.Map<UserProfileResponse>(user)
        };
        return result;
    }

    public async Task<string> RefreshToken()
    {
        var user = await _userRepository.GetByIdAsync(_currentUser.GetCurrentUserId());
        var accessToken = _tokenService.GenerateToken(user);
        _cookieService.Set(accessToken);

        return accessToken;
    }

    public async Task Update(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId);
        _mapper.Map(request, user);

        await _unitOfWork.ExecuteTransactionAsync(() =>
            _unitOfWork.UserRepository.Update(user), cancellationToken);
    }

    public async Task<UploadAvatarResponse> UploadAvatar(IFormFile file)
    {

        return new UploadAvatarResponse
        {
            error = false,
            avatar = "Avatar uploaded successfully!"
        };
    }
}
