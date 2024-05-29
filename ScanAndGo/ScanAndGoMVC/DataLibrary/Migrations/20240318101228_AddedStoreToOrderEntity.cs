using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoreToOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_StoreId",
                table: "orders",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_stores_StoreId",
                table: "orders",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_stores_StoreId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_StoreId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "orders");

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "Id", "PaymentDate", "PaymentIntentId", "PaymentStatus", "SessionId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7475), "TestPaymentIntentId", 0, "SessionId1", 1 },
                    { 2, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7533), "TestPaymentIntentId", 0, "SessionId2", 1 },
                    { 3, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7537), "TestPaymentIntentId", 0, "SessionId3", 1 },
                    { 4, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7541), "TestPaymentIntentId", 0, "SessionId4", 1 },
                    { 5, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7545), "TestPaymentIntentId", 0, "SessionId5", 1 },
                    { 6, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7549), "TestPaymentIntentId", 0, "SessionId6", 1 },
                    { 7, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7553), "TestPaymentIntentId", 0, "SessionId7", 1 },
                    { 8, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7558), "TestPaymentIntentId", 0, "SessionId8", 1 },
                    { 9, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7562), "TestPaymentIntentId", 0, "SessionId9", 1 },
                    { 10, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7567), "TestPaymentIntentId", 0, "SessionId10", 1 },
                    { 11, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7571), "TestPaymentIntentId", 0, "SessionId11", 1 },
                    { 12, new DateTime(2024, 4, 26, 8, 48, 21, 655, DateTimeKind.Local).AddTicks(7575), "TestPaymentIntentId", 0, "SessionId12", 1 }
                });
        }
    }
}
