using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stores_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Store",
                        column: x => x.StoreId,
                        principalTable: "stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "shoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shoppingCartItems_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_shoppingCartItems_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shoppingCartItems_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "stores",
                columns: new[] { "Id", "Address", "Name", "UserId" },
                values: new object[] { 1, "Saint Patrick", "Target", null });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "Name", "Password", "ResetPasswordExpire", "ResetPasswordToken", "UserType" },
                values: new object[] { 1, "user1@gmail.com", "User1Name", "AQAAAAIAAYagAAAAELQKZC7R2WQxHd1uo0g3xQ2YiBvZtuglIUcqhsekqrBVG8u6+gUBNaXjTerGZoIUwQ==", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0 });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "Id", "PaymentDate", "PaymentIntentId", "PaymentStatus", "SessionId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8026), "TestPaymentIntentId", 0, "SessionId1", 1 },
                    { 2, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8082), "TestPaymentIntentId", 0, "SessionId2", 1 },
                    { 3, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8086), "TestPaymentIntentId", 0, "SessionId3", 1 },
                    { 4, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8090), "TestPaymentIntentId", 0, "SessionId4", 1 },
                    { 5, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8094), "TestPaymentIntentId", 0, "SessionId5", 1 },
                    { 6, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8097), "TestPaymentIntentId", 0, "SessionId6", 1 },
                    { 7, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8101), "TestPaymentIntentId", 0, "SessionId7", 1 },
                    { 8, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8105), "TestPaymentIntentId", 0, "SessionId8", 1 },
                    { 9, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8109), "TestPaymentIntentId", 0, "SessionId9", 1 },
                    { 10, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8112), "TestPaymentIntentId", 0, "SessionId10", 1 },
                    { 11, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8116), "TestPaymentIntentId", 0, "SessionId11", 1 },
                    { 12, new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8120), "TestPaymentIntentId", 0, "SessionId12", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_products_StoreId",
                table: "products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCartItems_OrderId",
                table: "shoppingCartItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCartItems_ProductId",
                table: "shoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCartItems_UserId",
                table: "shoppingCartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_UserId",
                table: "stores",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoppingCartItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
