using ecommerce.Application.Services;

namespace ecommerce.Application.Common.Interfaces;

public interface IProductService
{
    public Task<ProductResponse> Get(int? pageIndex, int? pageSize);
    public Task<ProductSearchResponse> Search(string? searchQuery);
}
