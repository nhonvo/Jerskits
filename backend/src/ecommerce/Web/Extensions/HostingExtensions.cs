using ecommerce.Application;
using ecommerce.Application.Common;
using ecommerce.Infrastructure;
using ecommerce.Infrastructure.Data;
using ecommerce.Web.Middlewares;

namespace ecommerce.Web.Extensions
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder, AppSettings configuration)
        {
            builder.Services.AddInfrastructuresService(configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddWebAPIService(configuration);

            return builder.Build();
        }

        public static async Task<WebApplication> ConfigurePipeline(this WebApplication app, AppSettings configuration)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {

            });
            using var scope = app.Services.CreateScope();
            if (!configuration.UseInMemoryDatabase)
            {
                var initialize = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
                await initialize.InitializeAsync();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseMiddleware<PerformanceMiddleware>();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseCors("AllowSpecificOrigin");

            app.UseResponseCompression();

            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.ConfigureHealthCheck();

            app.ConfigureExceptionHandler(loggerFactory.CreateLogger("Exceptions"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
