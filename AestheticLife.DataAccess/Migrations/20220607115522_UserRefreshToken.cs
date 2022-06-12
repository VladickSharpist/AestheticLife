using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AestheticLife.DataAccess.Migrations
{
    public partial class UserRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActualRefreshToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualRefreshToken",
                table: "User");
        }
    }
}
