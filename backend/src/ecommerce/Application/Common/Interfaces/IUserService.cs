using ecommerce.Application.Common.Models.Response;
using ecommerce.Application.Common.Models.User;

namespace ecommerce.Application.Common.Interfaces;

public interface IUserService
{
    public Task<ResultResponse<SignInResponse>> SignIn(LoginRequest request);
    Task<UserDTO> SignUp(RegisterRequest request, CancellationToken token);
    void Logout();
    Task<string> RefreshToken();
    Task<ResultResponse<UserProfileResponse>> GetProfile();
    Task Update(UserUpdateRequest request, CancellationToken cancellationToken);
}
