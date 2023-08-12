using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSST.Migrations
{
    public partial class AddFlavorIdToTreats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlavorId",
                table: "Treats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlavorId",
                table: "Treats");
        }
    }
}
