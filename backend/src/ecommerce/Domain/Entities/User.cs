#nullable disable
namespace ecommerce.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class Information
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public Order Order { get; set; }
    }

    public class Delivery
    {
        public int Id { get; set; }
        public DateTime ArriveTime { get; set; }
        public DeliveryType Type { get; set; } // "standard" or "express"
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int InformationId { get; set; }
        public Information Information { get; set; }
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public int PaymentId { get; set; }
        public PaymentWithoutUser Payment { get; set; }
        public int OrderItemId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Price { get; set; }
    }

    public class PaymentWithoutUser
    {
        public int Id { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int CVV { get; set; }
        public Order Order { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int CVV { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; } //nike, adidas, jordan
        public ProductType Type { get; set; } // football, basketball
        public decimal Price { get; set; }
        public decimal? OffPrice { get; set; }
        public Gender Gender { get; set; }//men, women, kid
        public List<string> Color { get; set; }
        public List<string> Size { get; set; }
        public string Slug { get; set; }
        public List<string> Gallery { get; set; }
        public string Poster { get; set; }
        public DetailProduct DetailProduct { get; set; }
        public List<BagItem> BagItems { get; set; }
        public List<Review> Reviews { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Favorite> Favorites { get; set; }
    }

    public class DetailProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Specification { get; set; }
    }

    public class BagItem
    {
        public int Id { get; set; }
        public int BagId { get; set; }
        public Bag Bag { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public decimal Total { get; set; }
    }

    public class Bag
    {
        public int Id { get; set; }
        public List<BagItem> BagItems { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SaveAddress { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public Bag Bag { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Favorite> Favorites { get; set; }
        public Order Order { get; set; }
        public Role Role { get; set; } = Role.User;
    }
}
