using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJCWebApp.Migrations
{
    public partial class UserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "UserProfile",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserProfile");
        }
    }
}
