using ecommerce.Application.Common.Models.User;

namespace ecommerce.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Authenticate(LoginRequest request);
        Task<UserDTO> Register(RegisterRequest request, CancellationToken token);
        void Logout();
        Task<string> Refresh();
        Task<UserProfile> Profile();
    }
}
