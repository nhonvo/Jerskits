using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
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
    }
}
