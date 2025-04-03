﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLTCCN.Models.Data;

#nullable disable

namespace QLTCCN.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("QLTCCN.Models.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("QLTCCN.Models.Data.DanhMuc", b =>
                {
                    b.Property<int>("MaDanhMuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDanhMuc"));

                    b.Property<string>("Loai")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaDanhMuc");

                    b.ToTable("DanhMuc");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.DauTu", b =>
                {
                    b.Property<int>("MaDauTu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDauTu"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("GiaTri")
                        .HasColumnType("decimal(15,2)");

                    b.Property<decimal>("GiaTriHienTai")
                        .HasColumnType("decimal(15,2)");

                    b.Property<int>("MaLoaiDauTu")
                        .HasColumnType("int");

                    b.Property<string>("MaNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Ngay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MaDauTu");

                    b.HasIndex("MaLoaiDauTu");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("DauTu");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.GiaoDich", b =>
                {
                    b.Property<int>("MaGiaoDich")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaGiaoDich"));

                    b.Property<int>("DanhMucMaDanhMuc")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LoaiGiaoDich")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("MaDanhMuc")
                        .HasColumnType("int");

                    b.Property<string>("MaNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaTaiKhoan")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayGiaoDich")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SoTien")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("MaGiaoDich");

                    b.HasIndex("DanhMucMaDanhMuc");

                    b.HasIndex("MaNguoiDung");

                    b.HasIndex("MaTaiKhoan");

                    b.ToTable("GiaoDich");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.LichSuTraNo", b =>
                {
                    b.Property<int>("MaTraNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTraNo"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("MaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayTra")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SoTienTra")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("MaTraNo");

                    b.HasIndex("MaNo");

                    b.ToTable("LichSuTraNo");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.LoaiDauTu", b =>
                {
                    b.Property<int>("MaLoaiDauTu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiDauTu"));

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaLoaiDauTu");

                    b.ToTable("LoaiDauTu");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.MucTieu", b =>
                {
                    b.Property<int>("MaMucTieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaMucTieu"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("HanChot")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaDanhMuc")
                        .HasColumnType("int");

                    b.Property<string>("MaNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SoTienHienTai")
                        .HasColumnType("decimal(15,2)");

                    b.Property<decimal>("SoTienMucTieu")
                        .HasColumnType("decimal(15,2)");

                    b.Property<string>("TenMucTieu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MaMucTieu");

                    b.HasIndex("MaDanhMuc");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("MucTieu");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.NguoiDung", b =>
                {
                    b.Property<string>("MaNguoiDung")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaNguoiDung");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.NoKhoanVay", b =>
                {
                    b.Property<int>("MaNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNo"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("KyHan")
                        .HasColumnType("int");

                    b.Property<decimal>("LaiSuat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MaNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTraTiepTheo")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SoTien")
                        .HasColumnType("decimal(15,2)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MaNo");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("NoKhoanVay");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.TaiKhoan", b =>
                {
                    b.Property<int>("MaTaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTaiKhoan"));

                    b.Property<string>("LoaiTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("MaNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SoDu")
                        .HasColumnType("decimal(15,2)");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaTaiKhoan");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.ThongBao", b =>
                {
                    b.Property<int>("MaThongBao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaThongBao"));

                    b.Property<bool>("DaDoc")
                        .HasColumnType("bit");

                    b.Property<string>("Loai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("MaLienKet")
                        .HasColumnType("int");

                    b.Property<string>("MaNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Ngay")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("MaThongBao");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("ThongBao");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QLTCCN.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QLTCCN.Models.Data.DauTu", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.LoaiDauTu", "LoaiDauTu")
                        .WithMany("DauTus")
                        .HasForeignKey("MaLoaiDauTu")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLTCCN.Models.Data.NguoiDung", "NguoiDung")
                        .WithMany("DauTus")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LoaiDauTu");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.GiaoDich", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.DanhMuc", "DanhMuc")
                        .WithMany("GiaoDichs")
                        .HasForeignKey("DanhMucMaDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QLTCCN.Models.Data.NguoiDung", "NguoiDung")
                        .WithMany("GiaoDichs")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLTCCN.Models.Data.TaiKhoan", "TaiKhoan")
                        .WithMany("GiaoDichs")
                        .HasForeignKey("MaTaiKhoan")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DanhMuc");

                    b.Navigation("NguoiDung");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.LichSuTraNo", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.NoKhoanVay", "NoKhoanVay")
                        .WithMany("LichSuTraNos")
                        .HasForeignKey("MaNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoKhoanVay");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.MucTieu", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.DanhMuc", "DanhMuc")
                        .WithMany("MucTieus")
                        .HasForeignKey("MaDanhMuc")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLTCCN.Models.Data.NguoiDung", "NguoiDung")
                        .WithMany("MucTieus")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DanhMuc");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.NoKhoanVay", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.NguoiDung", "NguoiDung")
                        .WithMany("NoKhoanVays")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.TaiKhoan", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.NguoiDung", "NguoiDung")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.ThongBao", b =>
                {
                    b.HasOne("QLTCCN.Models.Data.NguoiDung", "NguoiDung")
                        .WithMany("ThongBaos")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.DanhMuc", b =>
                {
                    b.Navigation("GiaoDichs");

                    b.Navigation("MucTieus");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.LoaiDauTu", b =>
                {
                    b.Navigation("DauTus");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.NguoiDung", b =>
                {
                    b.Navigation("DauTus");

                    b.Navigation("GiaoDichs");

                    b.Navigation("MucTieus");

                    b.Navigation("NoKhoanVays");

                    b.Navigation("TaiKhoans");

                    b.Navigation("ThongBaos");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.NoKhoanVay", b =>
                {
                    b.Navigation("LichSuTraNos");
                });

            modelBuilder.Entity("QLTCCN.Models.Data.TaiKhoan", b =>
                {
                    b.Navigation("GiaoDichs");
                });
#pragma warning restore 612, 618
        }
    }
}
