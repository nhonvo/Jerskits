namespace ecommerce.Application.Common.Interfaces
{
    public interface ISeedService
    {
        Task Seed(CancellationToken token);
    }
}
