using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Infrastructure.Migrations;

/// <inheritdoc />
public partial class addDetalProductBagandBagItemsUBagUserRelationship : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Bags_User_UserId",
            table: "Bags");

        migrationBuilder.DropIndex(
            name: "IX_Bags_UserId",
            table: "Bags");

        migrationBuilder.AlterColumn<int>(
            name: "UserId",
            table: "Bags",
            type: "integer",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);

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
            name: "IX_Bags_UserId",
            table: "Bags",
            column: "UserId",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Bags_User_UserId",
            table: "Bags",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Bags_User_UserId",
            table: "Bags");

        migrationBuilder.DropIndex(
            name: "IX_Bags_UserId",
            table: "Bags");

        migrationBuilder.AlterColumn<int>(
            name: "UserId",
            table: "Bags",
            type: "integer",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer");

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
            name: "IX_Bags_UserId",
            table: "Bags",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_Bags_User_UserId",
            table: "Bags",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id");
    }
}
