using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AestheticLife.DataAccess.Migrations
{
    public partial class addMiddleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "User",
                newName: "MiddleName");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEPWx8c33+6jl5tmxv179fUzWCbC2xwmW83hMnl/MWyyXV3m5xzWTALJf2V3Mkh5/MQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "User",
                newName: "SecondName");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEE3QS7hTwDmWeTfUughvug2z77qFC7q7m4GL8zavF0LaqckNdxMKtSv3azjlMOdDTA==");
        }
    }
}
