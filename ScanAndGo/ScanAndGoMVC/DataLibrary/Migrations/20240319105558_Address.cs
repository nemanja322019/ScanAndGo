using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3248));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3310));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3313));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3316));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3319));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3321));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3330));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 55, 58, 135, DateTimeKind.Local).AddTicks(3332));*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(533));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(536));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(539));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(542));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 11,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "orders",
                keyColumn: "Id",
                keyValue: 12,
                column: "PaymentDate",
                value: new DateTime(2024, 4, 30, 3, 22, 34, 962, DateTimeKind.Local).AddTicks(559));
        }
    }
}
