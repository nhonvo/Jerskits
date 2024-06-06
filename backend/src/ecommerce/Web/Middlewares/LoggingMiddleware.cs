using ecommerce.Application.Common;
using ecommerce.Application.Common.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace ecommerce.Web.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly AppSettings _appSettings;

    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, AppSettings appSettings)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        _appSettings = appSettings;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_appSettings.Logging.RequestResponse.IsEnabled)
        {
            bool isValidFormatRequest = await LogRequest(context);

            if (!isValidFormatRequest)
                await _next(context);
            else
                await LogResponse(context);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task<bool> LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();
        using var memStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(memStream);
        memStream.Seek(0, SeekOrigin.Begin);

        var requestAsText = await new StreamReader(memStream).ReadToEndAsync();
        context.Request.Body.Position = 0;

        try
        {
            if (context.Request.ContentType != null && context.Request.ContentType.Contains("multipart/form-data"))
            {
                ExecuteLogRequest(context.Request.Path, "Multipart form-data content");
                return false;
            }
            else
            {
                var requestJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(requestAsText));
                ExecuteLogRequest("Request", requestJson);
            }
        }
        catch (Exception exception)
        {
            ExecuteLogRequest("Request", requestAsText.Replace(System.Environment.NewLine, string.Empty));
            _logger.LogError($"Exception while logging request: {exception}");
            return false;
        }

        return true;
    }

    private void ExecuteLogRequest(string path, string logString)
    {
        LogHelper.LogRequest(_logger, path, logString, _appSettings.Logging.RequestResponse.IsEnabled);
    }

    private async Task LogResponse(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseAsText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        LogHelper.LogResponse(
            _logger,
            "Response",
            responseAsText,
            context.Response.StatusCode,
            _appSettings.Logging.RequestResponse.IsEnabled
        );

        await responseBody.CopyToAsync(originalBodyStream);
    }
}
