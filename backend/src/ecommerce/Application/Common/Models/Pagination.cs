namespace ecommerce.Application.Common.Models;

public class Pagination<T>
{
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPages
    {
        get
        {
            var temp = TotalItemsCount / PageSize;
            return TotalItemsCount % PageSize == 0 ? temp : temp;
        }
    }
    public int CurrentPage { get; set; }
    public bool Next => CurrentPage + 1 < TotalPages;
    public bool Previous => CurrentPage > 0;
    public ICollection<T>? Items { get; set; }
}
