using ecommerce.Application;
using ecommerce.Application.Common;
using ecommerce.Application.Repositories;
using ecommerce.Infrastructure.Data;
using ecommerce.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppSettings configuration)
    {
        if (configuration.UseInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ecommerce"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.ConnectionStrings.DefaultConnection));
        }

        // register services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }
}
