using ecommerce.Application.Common;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<AppSettings>() 
    ?? throw new UserFriendlyException(ErrorCode.Internal, "Internal error", "Can not found configuration");
builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
            builder => builder
                .WithOrigins("http://localhost:5173")
                .AllowCredentials() // Allow credentials
                .AllowAnyHeader()
                .AllowAnyMethod()));

builder.Services.AddSingleton(configuration);
var app = builder.ConfigureServices(configuration).ConfigurePipeline(configuration);
app.UseCors("AllowSpecificOrigin");
await app.RunAsync();
