using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class MakeTaiKhoanAndDanhMucNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_DanhMuc_DanhMucMaDanhMuc",
                table: "GiaoDich");

            migrationBuilder.AlterColumn<int>(
                name: "DanhMucMaDanhMuc",
                table: "GiaoDich",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_DanhMuc_DanhMucMaDanhMuc",
                table: "GiaoDich",
                column: "DanhMucMaDanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "MaDanhMuc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_DanhMuc_DanhMucMaDanhMuc",
                table: "GiaoDich");

            migrationBuilder.AlterColumn<int>(
                name: "DanhMucMaDanhMuc",
                table: "GiaoDich",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_DanhMuc_DanhMucMaDanhMuc",
                table: "GiaoDich",
                column: "DanhMucMaDanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "MaDanhMuc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
