using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Services;

namespace ecommerce.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ICurrentTime, CurrentTime>();

            return services;
        }
    }
}
