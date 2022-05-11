using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJCWebApp.Migrations
{
    public partial class Locations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Job");

            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Job",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_JobID",
                table: "JobApplication",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_UserProfileID",
                table: "JobApplication",
                column: "UserProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Job_JobID",
                table: "JobApplication",
                column: "JobID",
                principalTable: "Job",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_UserProfile_UserProfileID",
                table: "JobApplication",
                column: "UserProfileID",
                principalTable: "UserProfile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Job_JobID",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_UserProfile_UserProfileID",
                table: "JobApplication");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_JobID",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_UserProfileID",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Job",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
