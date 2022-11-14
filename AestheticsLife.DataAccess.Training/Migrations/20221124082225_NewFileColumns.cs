using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AestheticsLife.DataAccess.Training.Migrations
{
    public partial class NewFileColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Exercise",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDataReady",
                table: "Exercise",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "IsDataReady",
                table: "Exercise");
        }
    }
}
