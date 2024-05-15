namespace ecommerce.Web.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsCustom(this IServiceCollection services)
        {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173/", "http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });
            return services;
        }
    }
}
