using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJCWebApp.Migrations
{
    public partial class UserProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Job_LocationID",
                table: "Job",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Location_LocationID",
                table: "Job",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Location_LocationID",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_LocationID",
                table: "Job");
        }
    }
}
