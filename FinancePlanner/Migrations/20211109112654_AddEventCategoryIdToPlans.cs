using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePlanner.Migrations
{
    public partial class AddEventCategoryIdToPlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventCategoryId",
                table: "Plans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventCategoryId",
                table: "Plans");
        }
    }
}
