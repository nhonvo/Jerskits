namespace ecommerce.Infrastructure.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<List<string>> Suggestion(string searchQuery);
        public Task<List<Product>> Search(string searchQuery);
    }
}
