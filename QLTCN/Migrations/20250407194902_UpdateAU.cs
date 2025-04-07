using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SoDu",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "SurvivalMode",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SurvivalModeStartDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoDu",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SurvivalMode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SurvivalModeStartDate",
                table: "AspNetUsers");
        }
    }
}
