using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePlanner.Migrations
{
    public partial class adminuserseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PaidLeavesTotal", "PaidLeavesUser", "Password", "RegisteredAt", "UserName" },
                values: new object[] { 1, "simon.blnt93@gmail.com", "admin", "user", 0, 0, "adminadmin", new DateTime(2021, 4, 22, 16, 21, 36, 551, DateTimeKind.Local).AddTicks(7631), "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
