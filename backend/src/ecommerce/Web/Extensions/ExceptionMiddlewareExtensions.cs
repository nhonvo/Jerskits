using System.Net;
using ecommerce.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using static ecommerce.Domain.Constant;

namespace ecommerce.Web.Extensions;
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {

        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorId = Guid.NewGuid();

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    string errorMessage = string.Empty;
                    string errorCode = string.Empty;
                    bool error = false;

                    if (contextFeature.Error is UserFriendlyException userFriendlyException)
                    {
                        switch (userFriendlyException.ErrorCode)
                        {
                            case ErrorCode.NotFound:
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.NOT_FOUND}";
                                error = false;
                                break;
                            case ErrorCode.VersionConflict:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.VERSION_CONFLICT}";
                                error = false;
                                break;
                            case ErrorCode.ItemAlreadyExists:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.ITEM_ALREADY_EXISTS}";
                                error = false;
                                break;
                            case ErrorCode.Conflict:
                                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.CONFLICT}";
                                error = false;
                                break;
                            case ErrorCode.BadRequest:
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.BAD_REQUEST}";
                                error = false;
                                break;
                            case ErrorCode.Unauthorized:
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.UNAUTHORIZED}";
                                error = false;
                                break;
                            case ErrorCode.Internal:
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.INTERNAL_ERROR}";
                                error = false;
                                break;
                            case ErrorCode.UnprocessableEntity:
                                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.UNPROCESSABLE_ENTITY}";
                                error = false;
                                break;
                            default:
                                context.Response.StatusCode = 500;
                                errorMessage = userFriendlyException.UserFriendlyMessage;
                                errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.GENERAL_ERROR}";
                                error = false;
                                break;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        errorCode = $"{Constant.Application.Name}.{ErrorRespondCode.GENERAL_ERROR}";
                        errorMessage = "An error has occurred.";
                    }
                    await context.Response.WriteAsync($@"{{
                                                            ""error"": ""{error}"",
                                                            ""message"": ""{errorMessage}"",
                                                            ""errorCode"": ""{errorCode}""
                                                        }}");

                    logger.LogError($"ErrorId:{errorId} Exception:{contextFeature.Error}");
                }
            }
        });
    }
}
