using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Infrastructure.Migrations;

/// <inheritdoc />
public partial class addFavorite : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Favorite",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<int>(type: "integer", nullable: false),
                ProductId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Favorite", x => x.Id);
                table.ForeignKey(
                    name: "FK_Favorite_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Favorite_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "Color", "Gallery", "Size" },
            values: new object[] { new List<string> { "Black", "White" }, new List<string> { "image1.jpg", "image2.jpg" }, new List<string> { "8", "9", "10" } });

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "Color", "Gallery", "Size" },
            values: new object[] { new List<string> { "Pink", "White" }, new List<string> { "image3.jpg", "image4.jpg" }, new List<string> { "7", "8", "9" } });

        migrationBuilder.CreateIndex(
            name: "IX_Favorite_ProductId",
            table: "Favorite",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_Favorite_UserId",
            table: "Favorite",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Favorite");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "Color", "Gallery", "Size" },
            values: new object[] { new List<string> { "Black", "White" }, new List<string> { "image1.jpg", "image2.jpg" }, new List<string> { "8", "9", "10" } });

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "Color", "Gallery", "Size" },
            values: new object[] { new List<string> { "Pink", "White" }, new List<string> { "image3.jpg", "image4.jpg" }, new List<string> { "7", "8", "9" } });
    }
}
