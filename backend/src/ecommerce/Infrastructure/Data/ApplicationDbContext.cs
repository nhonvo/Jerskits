using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DetailProduct> DetailProducts { get; set; }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<BagItem> BagItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentWithoutUser> PaymentWithoutUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<User>().HasData(
                 new User
                 {
                     Id = 1,
                     FullName = "John Doe",
                     UserName = "johndoe",
                     Email = "johndoe@example.com",
                     Password = "password123",
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
                    Password = "password456",
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
            );
            builder.Entity<Product>().HasData(
                 new Product
                 {
                     Id = 1,
                     Name = "Nike Air Zoom Pegasus 37",
                     Brand = Brand.nike,
                     Type = ProductType.football,
                     Price = 120.00m,
                     OffPrice = 100.00m,
                     Gender = Gender.men,
                     Color = new List<string> { "Black", "White" },
                     Size = new List<string> { "8", "9", "10" },
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
                    Color = new List<string> { "Pink", "White" },
                    Size = new List<string> { "7", "8", "9" },
                    Slug = "adidas-ultraboost-21",
                    Gallery = new List<string> { "image3.jpg", "image4.jpg" },
                    Poster = "poster2.jpg"
                }
            );

        }
    }
}
