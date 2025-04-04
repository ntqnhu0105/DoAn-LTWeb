using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_AspNetUsers_ApplicationUserId",
                table: "GiaoDich");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_NguoiDung_MaNguoiDung",
                table: "GiaoDich");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "GiaoDich",
                newName: "NguoiDungMaNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_GiaoDich_ApplicationUserId",
                table: "GiaoDich",
                newName: "IX_GiaoDich_NguoiDungMaNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_AspNetUsers_MaNguoiDung",
                table: "GiaoDich",
                column: "MaNguoiDung",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_NguoiDung_NguoiDungMaNguoiDung",
                table: "GiaoDich",
                column: "NguoiDungMaNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "MaNguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_AspNetUsers_MaNguoiDung",
                table: "GiaoDich");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_NguoiDung_NguoiDungMaNguoiDung",
                table: "GiaoDich");

            migrationBuilder.RenameColumn(
                name: "NguoiDungMaNguoiDung",
                table: "GiaoDich",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_GiaoDich_NguoiDungMaNguoiDung",
                table: "GiaoDich",
                newName: "IX_GiaoDich_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_AspNetUsers_ApplicationUserId",
                table: "GiaoDich",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_NguoiDung_MaNguoiDung",
                table: "GiaoDich",
                column: "MaNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "MaNguoiDung",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
