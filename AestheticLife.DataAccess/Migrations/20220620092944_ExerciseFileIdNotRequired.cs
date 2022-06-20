using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AestheticLife.DataAccess.Migrations
{
    public partial class ExerciseFileIdNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_File_FileId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_FileId",
                table: "Exercise");

            migrationBuilder.AlterColumn<long>(
                name: "FileId",
                table: "Exercise",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_FileId",
                table: "Exercise",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_File_FileId",
                table: "Exercise",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_File_FileId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_FileId",
                table: "Exercise");

            migrationBuilder.AlterColumn<long>(
                name: "FileId",
                table: "Exercise",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
