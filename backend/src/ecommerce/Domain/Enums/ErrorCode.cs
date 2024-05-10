namespace ecommerce.Domain.Enums
{
    public enum ErrorCode
    {
        ItemAlreadyExists = 7,
        VersionConflict = 1, // NuGet package verions different
        NotFound = 2,
        BadRequest = 3,
        Conflict = 4,
        Other = 5,
        Unauthorized = 6,
        Internal = 0,
        UnprocessableEntity = 8
    }
}