using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJCWebApp.Migrations
{
    public partial class CVFileFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "CVFile");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUploaded",
                table: "CVFile",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "CVFile",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUploaded",
                table: "CVFile");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CVFile");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "CVFile",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
