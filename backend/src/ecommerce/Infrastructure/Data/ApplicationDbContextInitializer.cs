using ecommerce.Application.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infrastructure.Data
{
    public class ApplicationDbContextInitializer(ApplicationDbContext context, ILoggerFactory logger)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger _logger = logger.CreateLogger<ApplicationDbContextInitializer>();

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
                await SeedUser();
                await SeedProduct();
            }
            catch (Exception exception)
            {
                _logger.LogError("Migration error {exception}", exception);
                throw;
            }
        }
        public async Task SeedUser()
        {
            await _context.Users.AddRangeAsync(new List<User>(){
                new User
                 {
                    Id = 1,
                    FullName = "John Doe",
                    UserName = "johndoe",
                    Email = "johndoe@example.com",
                    Password = "password123".Hash(),
                    SaveAddress = true,
                    Address = "123 Main St",
                    PostalCode = 12345,
                    City = "City",
                    State = "State",
                    Country = "Country",
                    FirstName = "John",
                    LastName = "Doe",
                    Avatar = "avatar.jpg",
                    PhoneNumber = "1234567890",
                    ContactEmail = "contact@example.com",
                    Role = Role.Admin
                 },
                new User
                {
                    Id = 2,
                    FullName = "Jane Smith",
                    UserName = "janesmith",
                    Email = "janesmith@example.com",
                    Password = "password123".Hash(),
                    SaveAddress = true,
                    Address = "456 Elm St",
                    PostalCode = 54321,
                    City = "City",
                    State = "State",
                    Country = "Country",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Avatar = "avatar.jpg",
                    PhoneNumber = "0987654321",
                    ContactEmail = "contact@example.com"
                }
            });
        }
        public async Task SeedProduct()
        {
            await _context.Products.AddRangeAsync(new List<Product>(){
                new Product
                {
                    Id = 1,
                    Name = "Nike Air Zoom Pegasus 37",
                    Brand = Brand.nike,
                    Type = ProductType.football,
                    Price = 120.00m,
                    OffPrice = 100.00m,
                    Gender = Gender.men,
                    Color = ["Black", "White"],
                    Size = ["8", "9", "10"],
                    Slug = "nike-air-zoom-pegasus-37",
                    Gallery = new List<string> { "image1.jpg", "image2.jpg" },
                    Poster = "poster.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Adidas Ultraboost 21",
                    Brand = Brand.adidas,
                    Type = ProductType.basketball,
                    Price = 180.00m,
                    OffPrice = 150.00m,
                    Gender = Gender.women,
                    Color = ["Pink", "White"],
                    Size = ["7", "8", "9"],
                    Slug = "adidas-ultraboost-21",
                    Gallery = ["image3.jpg", "image4.jpg"],
                    Poster = "poster2.jpg"
                }
            });
        }
    }
}
