using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class FixGiaoDichDanhMucRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_MaDanhMuc",
                table: "GiaoDich",
                column: "MaDanhMuc");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_DanhMuc_MaDanhMuc",
                table: "GiaoDich",
                column: "MaDanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "MaDanhMuc",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_DanhMuc_MaDanhMuc",
                table: "GiaoDich");

            migrationBuilder.DropIndex(
                name: "IX_GiaoDich_MaDanhMuc",
                table: "GiaoDich");
        }
    }
}
