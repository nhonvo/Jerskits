namespace ecommerce.Application.Common.Models.User
{
    public class SignInResponse
    {
        public string _id { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public string address { get; set; }
        public int postalCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string avatar { get; set; }
    }
}