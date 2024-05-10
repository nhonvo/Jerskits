using ecommerce.Application.Common.Models;
using ecommerce.Application.Common.Models.Book;

namespace ecommerce.Application.Common.Interfaces
{
    public interface IBookService
    {
        Task<Pagination<Book>> Get(int pageIndex, int pageSize);
        Task<Book> Get(int id);
        Task<int> Add(BookDTO request, CancellationToken token);
        Task<int> Update(Book request, CancellationToken token);
        Task<int> Delete(int id, CancellationToken token);
    }
}
