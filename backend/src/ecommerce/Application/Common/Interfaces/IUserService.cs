using ecommerce.Application.Common.Models.Response;
using ecommerce.Application.Common.Models.User;

namespace ecommerce.Application.Common.Interfaces;
public interface IUserService
{
    public Task<ResultResponse<SignInResponse>> SignIn(LoginRequest request);
    Task<RegisterResponse> SignUp(RegisterRequest request, CancellationToken token);
    void Logout();
    Task<string> RefreshToken();
    Task<ResultResponse<UserProfileResponse>> GetProfile();
    Task<UploadAvatarResponse> UploadAvatar(IFormFile file);
    Task Update(UserUpdateRequest request, CancellationToken cancellationToken);
}
