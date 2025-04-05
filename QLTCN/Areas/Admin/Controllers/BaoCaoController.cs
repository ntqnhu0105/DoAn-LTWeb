using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using QLTCCN.Models.ViewModels;
using System.Threading.Tasks;

namespace QLTCCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BaoCaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaoCaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? thang, int? nam, string userId)
        {
            thang ??= DateTime.Now.Month;
            nam ??= DateTime.Now.Year;

            // Nếu không có userId, lấy người dùng đầu tiên
            if (string.IsNullOrEmpty(userId))
            {
                var firstUser = await _context.Users.FirstOrDefaultAsync();
                userId = firstUser?.Id;
            }

            if (string.IsNullOrEmpty(userId))
            {
                return View(new BaoCaoViewModel { Thang = thang.Value, Nam = nam.Value });
            }

            var giaoDich = await _context.GiaoDich
                .Where(g => g.MaNguoiDung == userId && g.NgayGiaoDich.Month == thang && g.NgayGiaoDich.Year == nam)
                .Include(g => g.DanhMuc)
                .ToListAsync();

            decimal tongThuNhap = giaoDich
                .Where(g => g.LoaiGiaoDich == "ThuNhap")
                .Sum(g => g.SoTien);

            decimal tongChiTieu = giaoDich
                .Where(g => g.LoaiGiaoDich == "ChiTieu")
                .Sum(g => g.SoTien);

            decimal soTienTietKiem = tongThuNhap - tongChiTieu;

            var baoCao = await _context.BaoCao
                .FirstOrDefaultAsync(b => b.MaNguoiDung == userId && b.Thang == thang && b.Nam == nam);

            if (baoCao == null)
            {
                baoCao = new BaoCao
                {
                    MaNguoiDung = userId,
                    Thang = thang.Value,
                    Nam = nam.Value,
                    TongThuNhap = tongThuNhap,
                    TongChiTieu = tongChiTieu,
                    SoTienTietKiem = soTienTietKiem,
                    NgayTao = DateTime.Now
                };
                _context.BaoCao.Add(baoCao);
            }
            else
            {
                baoCao.TongThuNhap = tongThuNhap;
                baoCao.TongChiTieu = tongChiTieu;
                baoCao.SoTienTietKiem = soTienTietKiem;
                _context.BaoCao.Update(baoCao);
            }
            await _context.SaveChangesAsync();

            var mucTieu = await _context.MucTieu
                .Where(m => m.MaNguoiDung == userId && m.HanChot.Month == thang && m.HanChot.Year == nam)
                .ToListAsync();

            var tienDoMucTieu = mucTieu
                .Select(m => new TienDoMucTieuViewModel
                {
                    TenMucTieu = m.TenMucTieu,
                    TienDo = m.SoTienMucTieu > 0 ? (m.SoTienHienTai / m.SoTienMucTieu * 100) : 0
                })
                .ToList();

            var noKhoanVay = await _context.NoKhoanVay
                .Where(n => n.MaNguoiDung == userId && (n.TrangThai == "HoatDong" || n.TrangThai == "QuaHan"))
                .Include(n => n.LichSuTraNos)
                .ToListAsync();

            decimal noPhaiTra = 0;
            foreach (var no in noKhoanVay)
            {
                decimal tongTienPhaiTra = no.SoTien * (1 + no.LaiSuat / 100);
                decimal tongTienDaTra = no.LichSuTraNos?.Sum(ls => ls.SoTienTra) ?? 0;
                noPhaiTra += tongTienPhaiTra - tongTienDaTra;
            }

            var dauTu = await _context.DauTu
                .Where(d => d.MaNguoiDung == userId && d.Ngay.Month == thang && d.Ngay.Year == nam)
                .ToListAsync();

            decimal loiNhuanDauTu = dauTu.Sum(d => d.GiaTriHienTai - d.GiaTri);

            var thuChiData = new { ThuNhap = (double)tongThuNhap, ChiTieu = (double)tongChiTieu };
            var thuChiDataJson = System.Text.Json.JsonSerializer.Serialize(thuChiData);

            var chiTieuTheoDanhMuc = giaoDich
                .Where(g => g.LoaiGiaoDich == "ChiTieu")
                .GroupBy(g => g.DanhMuc?.TenDanhMuc ?? "Không xác định")
                .Select(g => new
                {
                    TenDanhMuc = g.Key,
                    TongChiTieu = (double)g.Sum(x => x.SoTien)
                })
                .ToList();

            var danhMucLabels = chiTieuTheoDanhMuc.Select(d => d.TenDanhMuc).ToList();
            var danhMucData = chiTieuTheoDanhMuc.Select(d => d.TongChiTieu).ToList();
            var danhMucColors = new[] { "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF", "#FF9F40" };

            var danhMucLabelsJson = System.Text.Json.JsonSerializer.Serialize(danhMucLabels);
            var danhMucDataJson = System.Text.Json.JsonSerializer.Serialize(danhMucData);
            var danhMucColorsJson = System.Text.Json.JsonSerializer.Serialize(danhMucColors.Take(danhMucLabels.Count).ToList());

            var model = new BaoCaoViewModel
            {
                TongThuNhap = tongThuNhap,
                TongChiTieu = tongChiTieu,
                SoTienTietKiem = soTienTietKiem,
                NoPhaiTra = noPhaiTra,
                LoiNhuanDauTu = loiNhuanDauTu,
                Thang = thang.Value,
                Nam = nam.Value,
                TienDoMucTieu = tienDoMucTieu,
                ThuChiData = thuChiDataJson,
                DanhMucLabels = danhMucLabelsJson,
                DanhMucData = danhMucDataJson,
                DanhMucColors = danhMucColorsJson
            };

            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.SelectedUserId = userId;

            return View(model);
        }
    }
}