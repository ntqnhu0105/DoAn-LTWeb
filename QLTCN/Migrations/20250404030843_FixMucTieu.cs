using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class FixMucTieu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MucTieu_AspNetUsers_ApplicationUserId",
                table: "MucTieu");

            migrationBuilder.DropForeignKey(
                name: "FK_MucTieu_NguoiDung_MaNguoiDung",
                table: "MucTieu");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "MucTieu",
                newName: "NguoiDungMaNguoiDung");

            migrationBuilder.RenameIndex(
                name: "IX_MucTieu_ApplicationUserId",
                table: "MucTieu",
                newName: "IX_MucTieu_NguoiDungMaNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_MucTieu_AspNetUsers_MaNguoiDung",
                table: "MucTieu",
                column: "MaNguoiDung",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MucTieu_NguoiDung_NguoiDungMaNguoiDung",
                table: "MucTieu",
                column: "NguoiDungMaNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "MaNguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MucTieu_AspNetUsers_MaNguoiDung",
                table: "MucTieu");

            migrationBuilder.DropForeignKey(
                name: "FK_MucTieu_NguoiDung_NguoiDungMaNguoiDung",
                table: "MucTieu");

            migrationBuilder.RenameColumn(
                name: "NguoiDungMaNguoiDung",
                table: "MucTieu",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MucTieu_NguoiDungMaNguoiDung",
                table: "MucTieu",
                newName: "IX_MucTieu_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MucTieu_AspNetUsers_ApplicationUserId",
                table: "MucTieu",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MucTieu_NguoiDung_MaNguoiDung",
                table: "MucTieu",
                column: "MaNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "MaNguoiDung",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
