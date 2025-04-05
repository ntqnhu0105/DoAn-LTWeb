using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class ThongBaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongBaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThongBao/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thongBao = await _context.ThongBao
                .Where(t => t.MaNguoiDung == userId && !t.DaDoc)
                .OrderByDescending(t => t.Ngay)
                .ToListAsync();

            return View(thongBao);
        }

        // POST: ThongBao/MarkAsRead/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thongBao = await _context.ThongBao
                .FirstOrDefaultAsync(t => t.MaThongBao == id && t.MaNguoiDung == userId);

            if (thongBao == null)
            {
                return NotFound();
            }

            thongBao.DaDoc = true;
            _context.ThongBao.Update(thongBao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ThongBao/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thongBao = await _context.ThongBao
                .FirstOrDefaultAsync(t => t.MaThongBao == id && t.MaNguoiDung == userId);

            if (thongBao == null)
            {
                return NotFound();
            }

            return View(thongBao);
        }

        // POST: ThongBao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thongBao = await _context.ThongBao
                .FirstOrDefaultAsync(t => t.MaThongBao == id && t.MaNguoiDung == userId);

            if (thongBao == null)
            {
                return NotFound();
            }

            _context.ThongBao.Remove(thongBao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Phương thức tạo thông báo
        private async Task CreateThongBao(string noiDung, string loai, string userId, int? maLienKet = null)
        {
            // Lấy FullName từ AspNetUsers
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            var fullName = user?.FullName ?? "Người dùng";

            // Thêm FullName vào nội dung thông báo
            noiDung = $"Chào {fullName}, {noiDung}";

            var thongBao = new ThongBao
            {
                MaNguoiDung = userId,
                NoiDung = noiDung.Length > 500 ? noiDung.Substring(0, 500) : noiDung,
                Ngay = DateTime.Now,
                DaDoc = false,
                Loai = loai,
                MaLienKet = maLienKet
            };
            _context.ThongBao.Add(thongBao);
            await _context.SaveChangesAsync();
        }

        // Phương thức kiểm tra và tạo thông báo tự động
        public async Task CheckAndCreateNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"Kiểm tra thông báo cho người dùng: {userId}");

            // 1. Kiểm tra mục tiêu gần đến hạn
            var mucTieu = await _context.MucTieu
                .Where(m => m.MaNguoiDung == userId && m.TrangThai == "DangTienHanh")
                .ToListAsync();
            Console.WriteLine($"Số mục tiêu: {mucTieu.Count}");

            foreach (var mt in mucTieu)
            {
                Console.WriteLine($"Mục tiêu: {mt.TenMucTieu}, HanChot: {mt.HanChot}, TrangThai: {mt.TrangThai}");
                var daysUntilDeadline = (mt.HanChot - DateTime.Now).Days;
                Console.WriteLine($"Mục tiêu '{mt.TenMucTieu}': Còn {daysUntilDeadline} ngày");
                if (daysUntilDeadline <= 7 && daysUntilDeadline >= 0)
                {
                    var noiDung = $"mục tiêu '{mt.TenMucTieu}' sắp đến hạn (còn {daysUntilDeadline} ngày).";
                    Console.WriteLine($"Tạo thông báo: {noiDung}");
                    await CreateThongBao(noiDung, "Nhắc nhở", userId, mt.MaMucTieu);
                }
                else
                {
                    Console.WriteLine($"Mục tiêu '{mt.TenMucTieu}' không thỏa mãn điều kiện (daysUntilDeadline: {daysUntilDeadline})");
                }
            }

            // 2. Kiểm tra khoản vay sắp đáo hạn
            var noKhoanVay = await _context.NoKhoanVay
                .Where(n => n.MaNguoiDung == userId && (n.TrangThai == "HoatDong" || n.TrangThai == "QuaHan"))
                .ToListAsync();
            Console.WriteLine($"Số khoản vay: {noKhoanVay.Count}");

            foreach (var no in noKhoanVay)
            {
                var ngayDaoHan = no.NgayBatDau.AddMonths(no.KyHan);
                var daysUntilDue = (ngayDaoHan - DateTime.Now).Days;
                Console.WriteLine($"Khoản vay '{no.MaNo}': Còn {daysUntilDue} ngày");
                if (daysUntilDue <= 7 && daysUntilDue >= 0)
                {
                    var noiDung = $"khoản vay '{no.MaNo}' sắp đến hạn trả (còn {daysUntilDue} ngày).";
                    Console.WriteLine($"Tạo thông báo: {noiDung}");
                    await CreateThongBao(noiDung, "Cảnh báo", userId, no.MaNo);
                }
            }

            // 3. Kiểm tra biến động đầu tư
            var dauTu = await _context.DauTu
                .Where(d => d.MaNguoiDung == userId && d.TrangThai == "HoatDong")
                .Include(d => d.LoaiDauTu)
                .ToListAsync();
            Console.WriteLine($"Số đầu tư: {dauTu.Count}");

            foreach (var dt in dauTu)
            {
                var loiNhuan = dt.GiaTriHienTai - dt.GiaTri;
                var tyLeBienDong = dt.GiaTri > 0 ? (loiNhuan / dt.GiaTri * 100) : 0;
                Console.WriteLine($"Đầu tư '{dt.LoaiDauTu?.TenLoai}': Biến động {tyLeBienDong:F2}%");
                if (Math.Abs(tyLeBienDong) >= 10)
                {
                    var noiDung = loiNhuan >= 0
                        ? $"đầu tư '{dt.LoaiDauTu?.TenLoai}' có lợi nhuận {tyLeBienDong:F2}% (+{loiNhuan:N0} VNĐ)."
                        : $"đầu tư '{dt.LoaiDauTu?.TenLoai}' đang lỗ {Math.Abs(tyLeBienDong):F2}% (-{Math.Abs(loiNhuan):N0} VNĐ).";
                    Console.WriteLine($"Tạo thông báo: {noiDung}");
                    await CreateThongBao(noiDung, "Cập nhật", userId, dt.MaDauTu);
                }
            }
        }
    }
}