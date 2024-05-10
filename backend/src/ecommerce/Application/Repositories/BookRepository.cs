using ecommerce.Infrastructure.Data;
using ecommerce.Infrastructure.Interface;

namespace ecommerce.Application.Repositories
{
    public class BookRepository(ApplicationDbContext context) : GenericRepository<Book>(context), IBookRepository
    {
    }
}
