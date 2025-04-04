using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;

namespace QLTCCN.Controllers
{
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactions = await _context.GiaoDich
                .Where(t => t.MaNguoiDung == userId)
                .Include(t => t.DanhMuc)
                .Include(t => t.TaiKhoan)
                .OrderByDescending(t => t.NgayGiaoDich)
                .ToListAsync();

            // Debug
            foreach (var t in transactions)
            {
                Console.WriteLine($"MaGiaoDich: {t.MaGiaoDich}, MaDanhMuc: {t.MaDanhMuc}, DanhMuc: {(t.DanhMuc != null ? t.DanhMuc.TenDanhMuc : "null")}");
            }

            return View(transactions);
        }   

        // GET: Transaction/Create - Hiển thị form thêm giao dịch
        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Dropdown cho DanhMuc
            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc");

            // Dropdown cho TaiKhoan
            ViewBag.TaiKhoan = new SelectList(
                _context.TaiKhoan
                    .Where(t => t.MaNguoiDung == userId)
                    .Select(t => new { t.MaTaiKhoan, Ten = $"{t.TenTaiKhoan} ({t.LoaiTaiKhoan})" }),
                "MaTaiKhoan",
                "Ten"
            );

            // Dropdown cho LoaiGiaoDich (cố định)
            ViewBag.LoaiGiaoDich = new SelectList(
                new[]
                {
                    new { Value = "ThuNhap", Text = "Thu nhập" },
                    new { Value = "ChiTieu", Text = "Chi tiêu" }
                },
                "Value",
                "Text"
            );

            return View();
        }

        // POST: Transaction/Create - Thêm giao dịch mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoTien,LoaiGiaoDich,MaTaiKhoan,MaDanhMuc,NgayGiaoDich,GhiChu")] GiaoDich giaoDich)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            giaoDich.MaNguoiDung = userId;

            // Xóa validation của MaNguoiDung khỏi ModelState
            ModelState.Remove("MaNguoiDung");

            Console.WriteLine($"MaNguoiDung: {giaoDich.MaNguoiDung}");
            Console.WriteLine($"SoTien: {giaoDich.SoTien}");
            Console.WriteLine($"LoaiGiaoDich: {giaoDich.LoaiGiaoDich}");
            Console.WriteLine($"MaTaiKhoan: {giaoDich.MaTaiKhoan}");
            Console.WriteLine($"MaDanhMuc: {giaoDich.MaDanhMuc}");
            Console.WriteLine($"NgayGiaoDich: {giaoDich.NgayGiaoDich}");

            if (ModelState.IsValid)
            {
                try
                {
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
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi lưu giao dịch: {ex.Message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            // Hiển thị lỗi nếu có
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
                Console.WriteLine($"ModelState Error: {error}");
            }

            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc");
            ViewBag.TaiKhoan = new SelectList(
                _context.TaiKhoan
                    .Where(t => t.MaNguoiDung == userId)
                    .Select(t => new { t.MaTaiKhoan, Ten = $"{t.TenTaiKhoan} ({t.LoaiTaiKhoan})" }),
                "MaTaiKhoan",
                "Ten"
            );
            ViewBag.LoaiGiaoDich = new SelectList(
                new[]
                {
            new { Value = "ThuNhap", Text = "Thu nhập" },
            new { Value = "ChiTieu", Text = "Chi tiêu" }
                },
                "Value",
                "Text",
                giaoDich.LoaiGiaoDich
            );

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

            // Debug chi tiết
            Console.WriteLine($"Edit GET - MaGiaoDich: {giaoDich.MaGiaoDich}");
            Console.WriteLine($"SoTien: {giaoDich.SoTien}");
            Console.WriteLine($"LoaiGiaoDich: {giaoDich.LoaiGiaoDich}");
            Console.WriteLine($"MaTaiKhoan: {giaoDich.MaTaiKhoan}");
            Console.WriteLine($"MaDanhMuc: {giaoDich.MaDanhMuc}");
            Console.WriteLine($"NgayGiaoDich: {giaoDich.NgayGiaoDich}");
            Console.WriteLine($"GhiChu: {giaoDich.GhiChu}");

            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc", giaoDich.MaDanhMuc);
            ViewBag.TaiKhoan = new SelectList(
                _context.TaiKhoan
                    .Where(t => t.MaNguoiDung == userId)
                    .Select(t => new { t.MaTaiKhoan, Ten = $"{t.TenTaiKhoan} ({t.LoaiTaiKhoan})" }),
                "MaTaiKhoan",
                "Ten",
                giaoDich.MaTaiKhoan
            );
            ViewBag.LoaiGiaoDich = new SelectList(
                new[]
                {
            new { Value = "ThuNhap", Text = "Thu nhập" },
            new { Value = "ChiTieu", Text = "Chi tiêu" }
                },
                "Value",
                "Text",
                giaoDich.LoaiGiaoDich
            );

            return View(giaoDich);
        }

        // POST: Transaction/Edit/5 - Cập nhật giao dịch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaGiaoDich,SoTien,LoaiGiaoDich,MaTaiKhoan,MaDanhMuc,NgayGiaoDich,GhiChu,MaNguoiDung")] GiaoDich giaoDich)
        {
            if (id != giaoDich.MaGiaoDich)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (giaoDich.MaNguoiDung != userId)
            {
                return Forbid();
            }

            ModelState.Remove("MaNguoiDung");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingGiaoDich = await _context.GiaoDich
                        .FirstOrDefaultAsync(t => t.MaGiaoDich == id && t.MaNguoiDung == userId);

                    if (existingGiaoDich == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật số dư tài khoản
                    var taiKhoan = await _context.TaiKhoan.FindAsync(giaoDich.MaTaiKhoan);
                    if (taiKhoan != null)
                    {
                        // Hoàn tác giao dịch cũ
                        if (existingGiaoDich.LoaiGiaoDich == "ThuNhap")
                            taiKhoan.SoDu -= existingGiaoDich.SoTien;
                        else if (existingGiaoDich.LoaiGiaoDich == "ChiTieu")
                            taiKhoan.SoDu += existingGiaoDich.SoTien;

                        // Áp dụng giao dịch mới
                        if (giaoDich.LoaiGiaoDich == "ThuNhap")
                            taiKhoan.SoDu += giaoDich.SoTien;
                        else if (giaoDich.LoaiGiaoDich == "ChiTieu")
                            taiKhoan.SoDu -= giaoDich.SoTien;

                        _context.TaiKhoan.Update(taiKhoan);
                    }

                    // Cập nhật giao dịch
                    existingGiaoDich.SoTien = giaoDich.SoTien;
                    existingGiaoDich.LoaiGiaoDich = giaoDich.LoaiGiaoDich;
                    existingGiaoDich.MaTaiKhoan = giaoDich.MaTaiKhoan;
                    existingGiaoDich.MaDanhMuc = giaoDich.MaDanhMuc;
                    existingGiaoDich.NgayGiaoDich = giaoDich.NgayGiaoDich;
                    existingGiaoDich.GhiChu = giaoDich.GhiChu;

                    _context.GiaoDich.Update(existingGiaoDich);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật giao dịch: {ex.Message}");
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
                Console.WriteLine($"ModelState Error: {error}");
            }

            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc", giaoDich.MaDanhMuc);
            ViewBag.TaiKhoan = new SelectList(
                _context.TaiKhoan
                    .Where(t => t.MaNguoiDung == userId)
                    .Select(t => new { t.MaTaiKhoan, Ten = $"{t.TenTaiKhoan} ({t.LoaiTaiKhoan})" }),
                "MaTaiKhoan",
                "Ten",
                giaoDich.MaTaiKhoan
            );
            ViewBag.LoaiGiaoDich = new SelectList(
                new[]
                {
                    new { Value = "ThuNhap", Text = "Thu nhập" },
                    new { Value = "ChiTieu", Text = "Chi tiêu" }
                },
                "Value",
                "Text",
                giaoDich.LoaiGiaoDich
            );

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

            if (giaoDich == null)
            {
                return NotFound();
            }

            try
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi xóa giao dịch: {ex.Message}");
                return View(giaoDich); // Trả lại view Delete với lỗi
            }
        }
    }
}