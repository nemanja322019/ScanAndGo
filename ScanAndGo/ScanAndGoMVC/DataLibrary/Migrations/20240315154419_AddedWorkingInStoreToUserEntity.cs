using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkingInStoreToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreUser");

            migrationBuilder.AddColumn<int>(
                name: "WorkingInStoreId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 883, DateTimeKind.Local).AddTicks(9923));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 883, DateTimeKind.Local).AddTicks(9991));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 883, DateTimeKind.Local).AddTicks(9996));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(28));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(32));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(38));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 8, 44, 18, 884, DateTimeKind.Local).AddTicks(42));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "WorkingInStoreId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_users_WorkingInStoreId",
                table: "users",
                column: "WorkingInStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_stores_WorkingInStoreId",
                table: "users",
                column: "WorkingInStoreId",
                principalTable: "stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_stores_WorkingInStoreId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_WorkingInStoreId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "WorkingInStoreId",
                table: "users");

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
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2863));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2924));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2927));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2931));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2938));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2941));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2945));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2949));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 26, 4, 18, 39, 508, DateTimeKind.Local).AddTicks(2956));

            migrationBuilder.CreateIndex(
                name: "IX_StoreUser_StoresId",
                table: "StoreUser",
                column: "StoresId");
        }
    }
}
