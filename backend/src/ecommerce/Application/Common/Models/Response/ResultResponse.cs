namespace ecommerce.Application.Common.Models.Response
{
    public class ResultResponse<T>
    {
        public bool error { get; set; }
        public string? message { get; set; }
        public T data { get; set; }
    }
}