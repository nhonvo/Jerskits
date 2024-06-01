namespace ecommerce.Application.Common.Models.User;

public class UserUpdateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactEmail { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public bool SaveAddress { get; set; }
}
public class UserUpdateAvatarRequest
{
    public string AvatarFileName { get; set; }
}
