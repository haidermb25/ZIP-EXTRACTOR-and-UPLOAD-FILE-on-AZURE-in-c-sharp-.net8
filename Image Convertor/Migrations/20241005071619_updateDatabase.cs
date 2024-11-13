using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Image_Convertor.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pack");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pack");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pack");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Icons");

            migrationBuilder.DropColumn(
                name: "IconSize",
                table: "Icons");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "Icons");

            migrationBuilder.RenameColumn(
                name: "TotalIcons",
                table: "Pack",
                newName: "Payment");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Pack",
                newName: "PackType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payment",
                table: "Pack",
                newName: "TotalIcons");

            migrationBuilder.RenameColumn(
                name: "PackType",
                table: "Pack",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pack",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Pack",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pack",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Icons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IconSize",
                table: "Icons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "Icons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
