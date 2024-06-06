using ecommerce.Application.Common;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Application.Services;
using ecommerce.Web.Extensions;
using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<AppSettings>()
    ?? throw new UserFriendlyException(ErrorCode.Internal, "Internal error", "Can not found configuration");

builder.Services.AddSingleton(configuration);
// Add anti-forgery services
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});
var app = await builder.ConfigureServices(configuration).ConfigurePipeline(configuration);

// Use anti-forgery
app.Use(next => context =>
{
    var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
    var tokens = antiforgery.GetAndStoreTokens(context);
    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!,
        new CookieOptions { HttpOnly = false });
    return next(context);
});

app.UseDefaultFiles();
app.UseStaticFiles();
await app.RunAsync();
