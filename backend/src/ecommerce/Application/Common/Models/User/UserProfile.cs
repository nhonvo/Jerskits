namespace ecommerce.Application.Common.Models.User
{
    public class UserProfileResponse
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SaveAddress { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public UserProfileResponseCity City { get; set; }
        public UserProfileResponseState State { get; set; }
        public UserProfileResponseCountry Country { get; set; }
        public Role Role { get; set; } = Role.User;
    }
    public class UserProfileResponseCity
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class UserProfileResponseCountry
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class UserProfileResponseState
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

}