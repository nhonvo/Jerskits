using ecommerce.Domain.Constants;

namespace ecommerce.Application.Common.Exceptions;

public static class UserException
{
    public static UserFriendlyException UserAlreadyExistsException(string field)
        => new(ErrorCode.BadRequest, UserErrorMessage.AlreadyExists);

    public static UserFriendlyException UnauthorizedException()
        => new(ErrorCode.Unauthorized, UserErrorMessage.Unauthorized);

    public static UserFriendlyException InternalException(Exception? exception)
        => new(ErrorCode.Internal, ErrorMessage.Internal, exception.Message);

    public static UserFriendlyException BadRequestException(string errorMessage)
        => new(ErrorCode.BadRequest, errorMessage);

}
