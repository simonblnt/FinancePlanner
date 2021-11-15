using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePlanner.Migrations
{
    public partial class AddedEventStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanStatuses",
                table: "PlanStatuses");

            migrationBuilder.RenameTable(
                name: "PlanStatuses",
                newName: "PlanStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanStatus",
                table: "PlanStatus",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanStatus",
                table: "PlanStatus");

            migrationBuilder.RenameTable(
                name: "PlanStatus",
                newName: "PlanStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanStatuses",
                table: "PlanStatuses",
                column: "Id");
        }
    }
}
