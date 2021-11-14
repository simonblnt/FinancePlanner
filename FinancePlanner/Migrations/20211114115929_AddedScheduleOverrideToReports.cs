using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePlanner.Migrations
{
    public partial class AddedScheduleOverrideToReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ScheduleOverride",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleOverride",
                table: "Reports");
        }
    }
}
