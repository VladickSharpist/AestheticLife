using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AestheticLife.DataAccess.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_File_VideoId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_VideoId",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Exercise",
                newName: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_FileId",
                table: "Exercise",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_File_FileId",
                table: "Exercise",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_File_FileId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_FileId",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Exercise",
                newName: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_VideoId",
                table: "Exercise",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_File_VideoId",
                table: "Exercise",
                column: "VideoId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
