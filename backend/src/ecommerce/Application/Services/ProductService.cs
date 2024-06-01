using ecommerce.Application.Common.Interfaces;

namespace ecommerce.Application.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ProductSearchResponse> Search(string? searchQuery)
    {
        var suggestions = await _unitOfWork.ProductRepository.Suggestion(searchQuery);
        var products = await _unitOfWork.ProductRepository.Search(searchQuery);

        return new ProductSearchResponse
        {
            Products = products,
            Suggestions = suggestions
        };
    }

    public async Task<ProductResponse> Get(int? pageIndex, int? pageSize)
    {
        var product = await _unitOfWork.ProductRepository.ToPagination(pageIndex ?? 0, pageSize ?? 10);
        var result = new ProductResponse
        {
            error = false,
            products = product.Items,
            totalPages = product.TotalPages,
            currentPage = product.CurrentPage,
            highestPrice = product.Items.OrderBy(x => x.Price).FirstOrDefault()
        };
        return result;
    }
}

public class ProductSearchResponse
{
    public List<Product> Products { get; set; }
    public List<string> Suggestions { get; set; }
}

public class ProductResponse
{
    public bool error { get; set; }
    public ICollection<Product> products { get; set; }
    public int totalPages { get; set; }
    public int currentPage { get; set; }
    public Product highestPrice { get; set; }
}
