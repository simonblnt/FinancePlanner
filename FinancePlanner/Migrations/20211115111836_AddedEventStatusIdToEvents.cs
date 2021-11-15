using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePlanner.Migrations
{
    public partial class AddedEventStatusIdToEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventStatusId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventStatusId",
                table: "Events");
        }
    }
}
