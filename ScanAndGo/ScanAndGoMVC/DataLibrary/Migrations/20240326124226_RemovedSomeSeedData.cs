using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSomeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "stores",
                columns: new[] { "Id", "Address", "Latitude", "Longitude", "Name", "UserId" },
                values: new object[] { 1, "Saint Patrick", 0.0, 0.0, "Target", null });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "IsVerified", "Name", "Password", "ResetPasswordExpire", "ResetPasswordToken", "TemporalPassword", "UserType", "VerificationCode", "VerifyEmailExpire", "WorkingInStoreId" },
                values: new object[] { 1, "user1@gmail.com", false, "User1Name", "AQAAAAIAAYagAAAAELQKZC7R2WQxHd1uo0g3xQ2YiBvZtuglIUcqhsekqrBVG8u6+gUBNaXjTerGZoIUwQ==", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
