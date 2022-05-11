using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJCWebApp.Migrations
{
    public partial class CVFileFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasCV",
                table: "JobApplication");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_CVFileID",
                table: "JobApplication",
                column: "CVFileID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_CVFile_CVFileID",
                table: "JobApplication",
                column: "CVFileID",
                principalTable: "CVFile",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_CVFile_CVFileID",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_CVFileID",
                table: "JobApplication");

            migrationBuilder.AddColumn<bool>(
                name: "HasCV",
                table: "JobApplication",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
