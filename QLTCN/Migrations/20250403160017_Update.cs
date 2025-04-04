using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_NguoiDung_MaNguoiDung",
                table: "TaiKhoan");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ThongBao",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiDungMaNguoiDung",
                table: "TaiKhoan",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "NoKhoanVay",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "MucTieu",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "GiaoDich",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DauTu",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_ApplicationUserId",
                table: "ThongBao",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_NguoiDungMaNguoiDung",
                table: "TaiKhoan",
                column: "NguoiDungMaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_NoKhoanVay_ApplicationUserId",
                table: "NoKhoanVay",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MucTieu_ApplicationUserId",
                table: "MucTieu",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_ApplicationUserId",
                table: "GiaoDich",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DauTu_ApplicationUserId",
                table: "DauTu",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DauTu_AspNetUsers_ApplicationUserId",
                table: "DauTu",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_AspNetUsers_ApplicationUserId",
                table: "GiaoDich",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MucTieu_AspNetUsers_ApplicationUserId",
                table: "MucTieu",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoKhoanVay_AspNetUsers_ApplicationUserId",
                table: "NoKhoanVay",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_AspNetUsers_MaNguoiDung",
                table: "TaiKhoan",
                column: "MaNguoiDung",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_NguoiDung_NguoiDungMaNguoiDung",
                table: "TaiKhoan",
                column: "NguoiDungMaNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "MaNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongBao_AspNetUsers_ApplicationUserId",
                table: "ThongBao",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DauTu_AspNetUsers_ApplicationUserId",
                table: "DauTu");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_AspNetUsers_ApplicationUserId",
                table: "GiaoDich");

            migrationBuilder.DropForeignKey(
                name: "FK_MucTieu_AspNetUsers_ApplicationUserId",
                table: "MucTieu");

            migrationBuilder.DropForeignKey(
                name: "FK_NoKhoanVay_AspNetUsers_ApplicationUserId",
                table: "NoKhoanVay");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_AspNetUsers_MaNguoiDung",
                table: "TaiKhoan");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_NguoiDung_NguoiDungMaNguoiDung",
                table: "TaiKhoan");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongBao_AspNetUsers_ApplicationUserId",
                table: "ThongBao");

            migrationBuilder.DropIndex(
                name: "IX_ThongBao_ApplicationUserId",
                table: "ThongBao");

            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_NguoiDungMaNguoiDung",
                table: "TaiKhoan");

            migrationBuilder.DropIndex(
                name: "IX_NoKhoanVay_ApplicationUserId",
                table: "NoKhoanVay");

            migrationBuilder.DropIndex(
                name: "IX_MucTieu_ApplicationUserId",
                table: "MucTieu");

            migrationBuilder.DropIndex(
                name: "IX_GiaoDich_ApplicationUserId",
                table: "GiaoDich");

            migrationBuilder.DropIndex(
                name: "IX_DauTu_ApplicationUserId",
                table: "DauTu");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ThongBao");

            migrationBuilder.DropColumn(
                name: "NguoiDungMaNguoiDung",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "NoKhoanVay");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "MucTieu");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "GiaoDich");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DauTu");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_NguoiDung_MaNguoiDung",
                table: "TaiKhoan",
                column: "MaNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "MaNguoiDung",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
