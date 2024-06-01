using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Utilities;
using ecommerce.Application.Services;
using ecommerce.Web.Services;

namespace ecommerce.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ILocationService, LocationService>();

        services.AddSingleton<ICurrentTime, CurrentTime>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<ICurrentUser, CurrentUser>();
        return services;
    }
}
