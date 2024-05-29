using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoreOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stores_users_UserId",
                table: "stores");

            migrationBuilder.CreateTable(
                name: "StoreUser",
                columns: table => new
                {
                    SellersId = table.Column<int>(type: "int", nullable: false),
                    StoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUser", x => new { x.SellersId, x.StoresId });
                    table.ForeignKey(
                        name: "FK_StoreUser_stores_StoresId",
                        column: x => x.StoresId,
                        principalTable: "stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreUser_users_SellersId",
                        column: x => x.SellersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(5930));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(5991));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(5994));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(5998));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6008));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6052));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6056));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6059));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 2, 51, 6, 703, DateTimeKind.Local).AddTicks(6062));

            migrationBuilder.CreateIndex(
                name: "IX_StoreUser_StoresId",
                table: "StoreUser",
                column: "StoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_stores_users_UserId",
                table: "stores",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stores_users_UserId",
                table: "stores");

            migrationBuilder.DropTable(
                name: "StoreUser");

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8026));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8097));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8109));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8112));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8116));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 25, 1, 38, 6, 228, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.AddForeignKey(
                name: "FK_stores_users_UserId",
                table: "stores",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
