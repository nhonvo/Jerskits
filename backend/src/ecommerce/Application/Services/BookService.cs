using AutoMapper;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models;
using ecommerce.Application.Common.Models.Book;

namespace ecommerce.Application.Services
{
    public class BookService(IUnitOfWork unitOfWork, IMapper mapper) : IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Pagination<Book>> Get(int pageIndex, int pageSize)
        {
            var books = await _unitOfWork.BookRepository.ToPagination(pageIndex, pageSize);
            return books;
        }

        public async Task<Book> Get(int id)
        {
            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<int> Add(BookDTO request, CancellationToken token)
        {
            var book = _mapper.Map<Book>(request);
            await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.BookRepository.AddAsync(book), token);
            return book.Id;
        }
        public async Task<int> Update(Book request, CancellationToken token)
        {
            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.BookRepository.Update(book), token);
            return book.Id;
        }
        public async Task<int> Delete(int id, CancellationToken token)
        {

            var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(x => x.Id == id);
            await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.BookRepository.Delete(book), token);
            return book.Id;
        }
    }
}
