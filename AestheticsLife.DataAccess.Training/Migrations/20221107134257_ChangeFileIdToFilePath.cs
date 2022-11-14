using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AestheticsLife.DataAccess.Training.Migrations
{
    public partial class ChangeFileIdToFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Exercise");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Exercise",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Exercise");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Exercise",
                type: "bigint",
                nullable: true);
        }
    }
}
