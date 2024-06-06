#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Application.Common;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Jwt Jwt { get; set; }
    public bool UseInMemoryDatabase { get; set; }
    public Logging Logging { get; set; }
    public CloudinarySettings Cloudinary { get; set; }
}

public class CloudinarySettings
{
    [Required]
    public string CloudName { get; set; } = string.Empty;
    [Required]
    public string ApiKey { get; set; } = string.Empty;
    [Required]
    public string ApiSecret { get; set; } = string.Empty;
}

public class ConnectionStrings
{
    [Required]
    public string DefaultConnection { get; set; }
}

public class Jwt
{
    [Required]
    public string Key { get; set; }
    [Required]
    public string Issuer { get; set; }
    [Required]
    public string Audience { get; set; }
}

public class Logging
{
    public RequestResponse RequestResponse { get; set; }
}

public class RequestResponse
{
    public bool IsEnabled { get; set; }
}
