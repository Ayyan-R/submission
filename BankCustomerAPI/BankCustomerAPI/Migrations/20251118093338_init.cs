using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankCustomerAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$j5vP5URd4d19JJiAGMgN3.mFDk89Kq0FFNWRRZe2d2lYutx4kG7Sq");

            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$tejzrVoo33gYMX.a1VYPxuZZuQ1ErBwO9mZsX3IjGqSlhxwIRxWaS");

            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$himFQ4jNEdW5H.CO49b67u.EXZXgSnTy1rS/rFh3uWNXv88IlHKsm");

            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$ezDLsO7MOUo12RCkb6rFpuQFAwcuE2xDsOwfKJI7An9iLdm3aBrdq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$qt/JJDd4PqgLSO60rsGiCe1kYVXukEPOsFzevfxtRY18gd/63i4Km");

            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$xeSYHpZ4y6gt/m.9SJUOu.sf197Jir6tRZOuKYr8hcTuOSLqyKAEq");

            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$cp2Rn8eu8dm./uQqAL6kAu6J6Sg5TPE8RyEhltw7z88kzIsMlwUEm");

            migrationBuilder.UpdateData(
                schema: "training",
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$4rn1xrAXwuyKMl.1cExEW.u6b8etYvIXBZ/.lHOyVv8lreoyi5iUy");
        }
    }
}
