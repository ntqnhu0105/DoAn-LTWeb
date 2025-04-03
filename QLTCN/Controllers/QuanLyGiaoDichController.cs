using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;

namespace QLTCCN.Controllers
{
    [Authorize] // Yêu cầu người dùng đăng nhập để truy cập
    public class QuanLyGiaoDichController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuanLyGiaoDichController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction/Index - Hiển thị danh sách giao dịch
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng từ Identity
            var transactions = await _context.GiaoDich
                .Where(t => t.MaNguoiDung == userId) // So sánh trực tiếp
                .Include(t => t.DanhMuc)
                .Include(t => t.TaiKhoan)
                .OrderByDescending(t => t.NgayGiaoDich)
                .ToListAsync();

            return View(transactions);
        }

        // GET: Transaction/Create - Hiển thị form thêm giao dịch
        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Lấy danh sách danh mục và tài khoản của người dùng hiện tại để hiển thị trong dropdown
            ViewBag.DanhMuc = _context.DanhMuc.ToList();
            ViewBag.TaiKhoan = _context.TaiKhoan
                .Where(t => t.MaNguoiDung == userId)
                .ToList();

            return View();
        }

        // POST: Transaction/Create - Thêm giao dịch mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GiaoDich giaoDich)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                giaoDich.MaNguoiDung = userId;

                // Cập nhật số dư tài khoản
                var taiKhoan = await _context.TaiKhoan.FindAsync(giaoDich.MaTaiKhoan);
                if (taiKhoan != null)
                {
                    if (giaoDich.LoaiGiaoDich == "ThuNhap")
                        taiKhoan.SoDu += giaoDich.SoTien;
                    else if (giaoDich.LoaiGiaoDich == "ChiTieu")
                        taiKhoan.SoDu -= giaoDich.SoTien;

                    _context.TaiKhoan.Update(taiKhoan);
                }

                _context.GiaoDich.Add(giaoDich);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, trả lại form với dữ liệu đã nhập
            ViewBag.DanhMuc = _context.DanhMuc.ToList();
            ViewBag.TaiKhoan = _context.TaiKhoan
                .Where(t => t.MaNguoiDung == (User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ToList();
            return View(giaoDich);
        }

        // GET: Transaction/Edit/5 - Hiển thị form sửa giao dịch
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var giaoDich = await _context.GiaoDich
                .FirstOrDefaultAsync(t => t.MaGiaoDich == id && t.MaNguoiDung == userId);

            if (giaoDich == null)
            {
                return NotFound();
            }

            ViewBag.DanhMuc = _context.DanhMuc.ToList();
            ViewBag.TaiKhoan = _context.TaiKhoan
                .Where(t => t.MaNguoiDung == userId)
                .ToList();

            return View(giaoDich);
        }

        // POST: Transaction/Edit/5 - Cập nhật giao dịch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GiaoDich giaoDich)
        {
            if (id != giaoDich.MaGiaoDich)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var existingTransaction = await _context.GiaoDich
                    .FirstOrDefaultAsync(t => t.MaGiaoDich == id && t.MaNguoiDung == userId);

                if (existingTransaction == null)
                {
                    return NotFound();
                }

                // Cập nhật số dư tài khoản (hoàn tác giao dịch cũ và áp dụng giao dịch mới)
                var taiKhoan = await _context.TaiKhoan.FindAsync(giaoDich.MaTaiKhoan);
                if (taiKhoan != null)
                {
                    if (existingTransaction.LoaiGiaoDich == "ThuNhap")
                        taiKhoan.SoDu -= existingTransaction.SoTien;
                    else if (existingTransaction.LoaiGiaoDich == "ChiTieu")
                        taiKhoan.SoDu += existingTransaction.SoTien;

                    if (giaoDich.LoaiGiaoDich == "ThuNhap")
                        taiKhoan.SoDu += giaoDich.SoTien;
                    else if (giaoDich.LoaiGiaoDich == "ChiTieu")
                        taiKhoan.SoDu -= giaoDich.SoTien;

                    _context.TaiKhoan.Update(taiKhoan);
                }

                // Cập nhật thông tin giao dịch
                existingTransaction.MaTaiKhoan = giaoDich.MaTaiKhoan;
                existingTransaction.MaDanhMuc = giaoDich.MaDanhMuc;
                existingTransaction.SoTien = giaoDich.SoTien;
                existingTransaction.LoaiGiaoDich = giaoDich.LoaiGiaoDich;
                existingTransaction.NgayGiaoDich = giaoDich.NgayGiaoDich;
                existingTransaction.GhiChu = giaoDich.GhiChu;

                _context.GiaoDich.Update(existingTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DanhMuc = _context.DanhMuc.ToList();
            ViewBag.TaiKhoan = _context.TaiKhoan
                .Where(t => t.MaNguoiDung == (User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ToList();
            return View(giaoDich);
        }

        // GET: Transaction/Delete/5 - Hiển thị xác nhận xóa giao dịch
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var giaoDich = await _context.GiaoDich
                .Include(t => t.DanhMuc)
                .Include(t => t.TaiKhoan)
                .FirstOrDefaultAsync(t => t.MaGiaoDich == id && t.MaNguoiDung == userId);

            if (giaoDich == null)
            {
                return NotFound();
            }

            return View(giaoDich);
        }

        // POST: Transaction/Delete/5 - Xóa giao dịch
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var giaoDich = await _context.GiaoDich
                .FirstOrDefaultAsync(t => t.MaGiaoDich == id && t.MaNguoiDung == userId);

            if (giaoDich != null)
            {
                // Hoàn tác số dư tài khoản
                var taiKhoan = await _context.TaiKhoan.FindAsync(giaoDich.MaTaiKhoan);
                if (taiKhoan != null)
                {
                    if (giaoDich.LoaiGiaoDich == "ThuNhap")
                        taiKhoan.SoDu -= giaoDich.SoTien;
                    else if (giaoDich.LoaiGiaoDich == "ChiTieu")
                        taiKhoan.SoDu += giaoDich.SoTien;

                    _context.TaiKhoan.Update(taiKhoan);
                }

                _context.GiaoDich.Remove(giaoDich);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
