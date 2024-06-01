namespace ecommerce.Web.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsCustom(this IServiceCollection services)
    {
        return services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
             builder => builder
                 .WithOrigins("http://localhost:5173")
                 .AllowCredentials() // Allow credentials
                 .AllowAnyHeader()
                 .AllowAnyMethod()));
    }
}
