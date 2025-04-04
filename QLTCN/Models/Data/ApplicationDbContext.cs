using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
namespace QLTCCN.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Định nghĩa các DbSet cho từng bảng
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<GiaoDich> GiaoDich { get; set; }
        public DbSet<MucTieu> MucTieu { get; set; }
        public DbSet<NoKhoanVay> NoKhoanVay { get; set; }
        public DbSet<LichSuTraNo> LichSuTraNo { get; set; }
        public DbSet<ThongBao> ThongBao { get; set; }
        public DbSet<LoaiDauTu> LoaiDauTu { get; set; }
        public DbSet<DauTu> DauTu { get; set; }
        public DbSet<BaoCao> BaoCao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GiaoDich>()
                .HasOne(g => g.DanhMuc)
                .WithMany() // Nếu DanhMuc không có List<GiaoDich>, để trống
                .HasForeignKey(g => g.MaDanhMuc);
            // Cấu hình mối quan hệ GiaoDich - TaiKhoan
            modelBuilder.Entity<GiaoDich>()
                .HasOne(g => g.TaiKhoan)
                .WithMany(t => t.GiaoDichs)
                .HasForeignKey(g => g.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Restrict); // Tắt CASCADE, không xóa GiaoDich khi xóa TaiKhoan

            // Cấu hình mối quan hệ GiaoDich - NguoiDung
            modelBuilder.Entity<GiaoDich>()
                .HasOne(g => g.NguoiDung)
                .WithMany(n => n.GiaoDichs)
                .HasForeignKey(g => g.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict); // Tắt CASCADE, không xóa GiaoDich khi xóa NguoiDung

            // Cấu hình mối quan hệ TaiKhoan - NguoiDung
            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.NguoiDung)
                .WithMany(n => n.TaiKhoans)
                .HasForeignKey(t => t.MaNguoiDung)
                .OnDelete(DeleteBehavior.Cascade); // Cho phép xóa TaiKhoan khi xóa NguoiDung

            // Cấu hình các mối quan hệ khác (nếu cần)
            modelBuilder.Entity<MucTieu>()
                .HasOne(m => m.NguoiDung)
                .WithMany(n => n.MucTieus)
                .HasForeignKey(m => m.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MucTieu>()
                .HasOne(m => m.DanhMuc)
                .WithMany(d => d.MucTieus)
                .HasForeignKey(m => m.MaDanhMuc)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NoKhoanVay>()
                .HasOne(n => n.NguoiDung)
                .WithMany(u => u.NoKhoanVays)
                .HasForeignKey(n => n.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LichSuTraNo>()
                .HasOne(l => l.NoKhoanVay)
                .WithMany(n => n.LichSuTraNos)
                .HasForeignKey(l => l.MaNo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThongBao>()
                .HasOne(t => t.NguoiDung)
                .WithMany(n => n.ThongBaos)
                .HasForeignKey(t => t.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DauTu>()
                .HasOne(d => d.NguoiDung)
                .WithMany(n => n.DauTus)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DauTu>()
                .HasOne(d => d.LoaiDauTu)
                .WithMany(l => l.DauTus)
                .HasForeignKey(d => d.MaLoaiDauTu)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BaoCao>(entity =>
            {
                // Cấu hình khóa ngoại MaNguoiDung với ApplicationUser
                entity.HasOne(b => b.NguoiDung)
                      .WithMany()
                      .HasForeignKey(b => b.MaNguoiDung)
                      .OnDelete(DeleteBehavior.Restrict);

                // Cấu hình giá trị mặc định cho NgayTao
                entity.Property(b => b.NgayTao)
                      .HasDefaultValueSql("GETDATE()");

                // Cấu hình giá trị mặc định cho các trường decimal
                entity.Property(b => b.TongThuNhap)
                      .HasDefaultValue(0m);

                entity.Property(b => b.TongChiTieu)
                      .HasDefaultValue(0m);

                entity.Property(b => b.SoTienTietKiem)
                      .HasDefaultValue(0m);
            });
        }

    }
}
