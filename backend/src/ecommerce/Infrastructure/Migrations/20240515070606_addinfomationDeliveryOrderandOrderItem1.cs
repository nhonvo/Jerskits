using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Infrastructure.Migrations;

/// <inheritdoc />
public partial class addinfomationDeliveryOrderandOrderItem1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Favorite_Products_ProductId",
            table: "Favorite");

        migrationBuilder.DropForeignKey(
            name: "FK_Favorite_User_UserId",
            table: "Favorite");

        migrationBuilder.DropForeignKey(
            name: "FK_Order_Delivery_DeliveryId",
            table: "Order");

        migrationBuilder.DropForeignKey(
            name: "FK_Order_Information_InformationId",
            table: "Order");

        migrationBuilder.DropForeignKey(
            name: "FK_Order_PaymentWithoutUsers_PaymentId",
            table: "Order");

        migrationBuilder.DropForeignKey(
            name: "FK_Order_User_UserId",
            table: "Order");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItem_Order_OrderId",
            table: "OrderItem");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItem_Products_ProductId",
            table: "OrderItem");

        migrationBuilder.DropPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Order",
            table: "Order");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Information",
            table: "Information");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Favorite",
            table: "Favorite");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Delivery",
            table: "Delivery");

        migrationBuilder.RenameTable(
            name: "OrderItem",
            newName: "OrderItems");

        migrationBuilder.RenameTable(
            name: "Order",
            newName: "Orders");

        migrationBuilder.RenameTable(
            name: "Information",
            newName: "Informations");

        migrationBuilder.RenameTable(
            name: "Favorite",
            newName: "Favorites");

        migrationBuilder.RenameTable(
            name: "Delivery",
            newName: "Deliveries");

        migrationBuilder.RenameIndex(
            name: "IX_OrderItem_ProductId",
            table: "OrderItems",
            newName: "IX_OrderItems_ProductId");

        migrationBuilder.RenameIndex(
            name: "IX_OrderItem_OrderId",
            table: "OrderItems",
            newName: "IX_OrderItems_OrderId");

        migrationBuilder.RenameIndex(
            name: "IX_Order_UserId",
            table: "Orders",
            newName: "IX_Orders_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_Order_PaymentId",
            table: "Orders",
            newName: "IX_Orders_PaymentId");

        migrationBuilder.RenameIndex(
            name: "IX_Order_InformationId",
            table: "Orders",
            newName: "IX_Orders_InformationId");

        migrationBuilder.RenameIndex(
            name: "IX_Order_DeliveryId",
            table: "Orders",
            newName: "IX_Orders_DeliveryId");

        migrationBuilder.RenameIndex(
            name: "IX_Favorite_UserId",
            table: "Favorites",
            newName: "IX_Favorites_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_Favorite_ProductId",
            table: "Favorites",
            newName: "IX_Favorites_ProductId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_OrderItems",
            table: "OrderItems",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Orders",
            table: "Orders",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Informations",
            table: "Informations",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Favorites",
            table: "Favorites",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Deliveries",
            table: "Deliveries",
            column: "Id");

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

        migrationBuilder.AddForeignKey(
            name: "FK_Favorites_Products_ProductId",
            table: "Favorites",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Favorites_User_UserId",
            table: "Favorites",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            table: "OrderItems",
            column: "OrderId",
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_Products_ProductId",
            table: "OrderItems",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Orders_Deliveries_DeliveryId",
            table: "Orders",
            column: "DeliveryId",
            principalTable: "Deliveries",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Orders_Informations_InformationId",
            table: "Orders",
            column: "InformationId",
            principalTable: "Informations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Orders_PaymentWithoutUsers_PaymentId",
            table: "Orders",
            column: "PaymentId",
            principalTable: "PaymentWithoutUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Orders_User_UserId",
            table: "Orders",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Favorites_Products_ProductId",
            table: "Favorites");

        migrationBuilder.DropForeignKey(
            name: "FK_Favorites_User_UserId",
            table: "Favorites");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            table: "OrderItems");

        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_Products_ProductId",
            table: "OrderItems");

        migrationBuilder.DropForeignKey(
            name: "FK_Orders_Deliveries_DeliveryId",
            table: "Orders");

        migrationBuilder.DropForeignKey(
            name: "FK_Orders_Informations_InformationId",
            table: "Orders");

        migrationBuilder.DropForeignKey(
            name: "FK_Orders_PaymentWithoutUsers_PaymentId",
            table: "Orders");

        migrationBuilder.DropForeignKey(
            name: "FK_Orders_User_UserId",
            table: "Orders");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Orders",
            table: "Orders");

        migrationBuilder.DropPrimaryKey(
            name: "PK_OrderItems",
            table: "OrderItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Informations",
            table: "Informations");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Favorites",
            table: "Favorites");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Deliveries",
            table: "Deliveries");

        migrationBuilder.RenameTable(
            name: "Orders",
            newName: "Order");

        migrationBuilder.RenameTable(
            name: "OrderItems",
            newName: "OrderItem");

        migrationBuilder.RenameTable(
            name: "Informations",
            newName: "Information");

        migrationBuilder.RenameTable(
            name: "Favorites",
            newName: "Favorite");

        migrationBuilder.RenameTable(
            name: "Deliveries",
            newName: "Delivery");

        migrationBuilder.RenameIndex(
            name: "IX_Orders_UserId",
            table: "Order",
            newName: "IX_Order_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_Orders_PaymentId",
            table: "Order",
            newName: "IX_Order_PaymentId");

        migrationBuilder.RenameIndex(
            name: "IX_Orders_InformationId",
            table: "Order",
            newName: "IX_Order_InformationId");

        migrationBuilder.RenameIndex(
            name: "IX_Orders_DeliveryId",
            table: "Order",
            newName: "IX_Order_DeliveryId");

        migrationBuilder.RenameIndex(
            name: "IX_OrderItems_ProductId",
            table: "OrderItem",
            newName: "IX_OrderItem_ProductId");

        migrationBuilder.RenameIndex(
            name: "IX_OrderItems_OrderId",
            table: "OrderItem",
            newName: "IX_OrderItem_OrderId");

        migrationBuilder.RenameIndex(
            name: "IX_Favorites_UserId",
            table: "Favorite",
            newName: "IX_Favorite_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_Favorites_ProductId",
            table: "Favorite",
            newName: "IX_Favorite_ProductId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Order",
            table: "Order",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Information",
            table: "Information",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Favorite",
            table: "Favorite",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Delivery",
            table: "Delivery",
            column: "Id");

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

        migrationBuilder.AddForeignKey(
            name: "FK_Favorite_Products_ProductId",
            table: "Favorite",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Favorite_User_UserId",
            table: "Favorite",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Order_Delivery_DeliveryId",
            table: "Order",
            column: "DeliveryId",
            principalTable: "Delivery",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Order_Information_InformationId",
            table: "Order",
            column: "InformationId",
            principalTable: "Information",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Order_PaymentWithoutUsers_PaymentId",
            table: "Order",
            column: "PaymentId",
            principalTable: "PaymentWithoutUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Order_User_UserId",
            table: "Order",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItem_Order_OrderId",
            table: "OrderItem",
            column: "OrderId",
            principalTable: "Order",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_OrderItem_Products_ProductId",
            table: "OrderItem",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
