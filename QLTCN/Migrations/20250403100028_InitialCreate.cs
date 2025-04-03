using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTCCN.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.MaDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "LoaiDauTu",
                columns: table => new
                {
                    MaLoaiDauTu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiDauTu", x => x.MaLoaiDauTu);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DauTu",
                columns: table => new
                {
                    MaDauTu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaLoaiDauTu = table.Column<int>(type: "int", nullable: false),
                    GiaTri = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    GiaTriHienTai = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DauTu", x => x.MaDauTu);
                    table.ForeignKey(
                        name: "FK_DauTu_LoaiDauTu_MaLoaiDauTu",
                        column: x => x.MaLoaiDauTu,
                        principalTable: "LoaiDauTu",
                        principalColumn: "MaLoaiDauTu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DauTu_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MucTieu",
                columns: table => new
                {
                    MaMucTieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    TenMucTieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoTienMucTieu = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    SoTienHienTai = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    HanChot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MucTieu", x => x.MaMucTieu);
                    table.ForeignKey(
                        name: "FK_MucTieu_DanhMuc_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MucTieu_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NoKhoanVay",
                columns: table => new
                {
                    MaNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    LaiSuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KyHan = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NgayTraTiepTheo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoKhoanVay", x => x.MaNo);
                    table.ForeignKey(
                        name: "FK_NoKhoanVay_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenTaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoDu = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    LoaiTaiKhoan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.MaTaiKhoan);
                    table.ForeignKey(
                        name: "FK_TaiKhoan_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    MaThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaDoc = table.Column<bool>(type: "bit", nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaLienKet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.MaThongBao);
                    table.ForeignKey(
                        name: "FK_ThongBao_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichSuTraNo",
                columns: table => new
                {
                    MaTraNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNo = table.Column<int>(type: "int", nullable: false),
                    SoTienTra = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuTraNo", x => x.MaTraNo);
                    table.ForeignKey(
                        name: "FK_LichSuTraNo_NoKhoanVay_MaNo",
                        column: x => x.MaNo,
                        principalTable: "NoKhoanVay",
                        principalColumn: "MaNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    MaGiaoDich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    LoaiGiaoDich = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayGiaoDich = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DanhMucMaDanhMuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.MaGiaoDich);
                    table.ForeignKey(
                        name: "FK_GiaoDich_DanhMuc_DanhMucMaDanhMuc",
                        column: x => x.DanhMucMaDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiaoDich_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiaoDich_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DauTu_MaLoaiDauTu",
                table: "DauTu",
                column: "MaLoaiDauTu");

            migrationBuilder.CreateIndex(
                name: "IX_DauTu_MaNguoiDung",
                table: "DauTu",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_DanhMucMaDanhMuc",
                table: "GiaoDich",
                column: "DanhMucMaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_MaNguoiDung",
                table: "GiaoDich",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_MaTaiKhoan",
                table: "GiaoDich",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuTraNo_MaNo",
                table: "LichSuTraNo",
                column: "MaNo");

            migrationBuilder.CreateIndex(
                name: "IX_MucTieu_MaDanhMuc",
                table: "MucTieu",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_MucTieu_MaNguoiDung",
                table: "MucTieu",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_NoKhoanVay_MaNguoiDung",
                table: "NoKhoanVay",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_MaNguoiDung",
                table: "TaiKhoan",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_MaNguoiDung",
                table: "ThongBao",
                column: "MaNguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DauTu");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "LichSuTraNo");

            migrationBuilder.DropTable(
                name: "MucTieu");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LoaiDauTu");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "NoKhoanVay");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
