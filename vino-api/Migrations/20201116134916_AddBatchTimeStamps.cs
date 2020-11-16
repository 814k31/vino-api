using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vino_api.Migrations
{
    public partial class AddBatchTimeStamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Batches");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Batches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Batches",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Batches");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Batches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
