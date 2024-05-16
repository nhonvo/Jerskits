using ecommerce.Application.Common;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<AppSettings>()
    ?? throw new UserFriendlyException(ErrorCode.Internal, "Internal error", "Can not found configuration");

builder.Services.AddSingleton(configuration);

var app = builder.ConfigureServices(configuration).ConfigurePipeline(configuration);

await app.RunAsync();
