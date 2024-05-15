using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addproducttableseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Color", "Gallery", "Gender", "Name", "OffPrice", "Poster", "Price", "Size", "Slug", "Type" },
                values: new object[,]
                {
                    { 1, 0, new[] { "Black", "White" }, new[] { "image1.jpg", "image2.jpg" }, 0, "Nike Air Zoom Pegasus 37", 100.00m, "poster.jpg", 120.00m, new[] { "8", "9", "10" }, "nike-air-zoom-pegasus-37", 0 },
                    { 2, 1, new[] { "Pink", "White" }, new[] { "image3.jpg", "image4.jpg" }, 1, "Adidas Ultraboost 21", 150.00m, "poster2.jpg", 180.00m, new[] { "7", "8", "9" }, "adidas-ultraboost-21", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
