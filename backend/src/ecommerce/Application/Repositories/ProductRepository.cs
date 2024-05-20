using System.Drawing;
using ecommerce.Infrastructure.Data;
using ecommerce.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Application.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<string>> Suggestion(string searchQuery)
            => await _context.Products.Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()))
                                        .Select(p => p.Name)
                                        .Distinct()
                                        .Take(5)
                                        .ToListAsync();

        public async Task<List<Product>> Search(string searchQuery)
            => await _context.Products
            .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()))
                    // || p.Brand == Enum.Parse<Brand>(searchQuery, true) 
                    // || p.Type == Enum.Parse<ProductType>(searchQuery, true) 
                    // || p.Gender == Enum.Parse<Gender>(searchQuery, true)
                    // || p.Color.ToString().ToLower().Contains(searchQuery.ToLower()))
            .Take(3).ToListAsync();
    }
}
