using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancePlanner.Migrations
{
    public partial class AddedEventStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EventStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStatuses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventStatuses");

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
    }
}
