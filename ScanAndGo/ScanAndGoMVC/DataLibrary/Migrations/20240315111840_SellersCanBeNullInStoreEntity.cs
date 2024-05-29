using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SellersCanBeNullInStoreEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
