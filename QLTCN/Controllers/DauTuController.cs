using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class DauTuController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public DauTuController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // GET: DauTu/Index
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int? maLoaiDauTu)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _context.DauTu
                .Where(d => d.MaNguoiDung == userId)
                .Include(d => d.LoaiDauTu)
                .AsQueryable();

            // Áp dụng bộ lọc thời gian
            if (startDate.HasValue)
            {
                query = query.Where(d => d.Ngay >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(d => d.Ngay <= endDate.Value);
            }

            // Áp dụng bộ lọc theo loại đầu tư
            if (maLoaiDauTu.HasValue)
            {
                query = query.Where(d => d.MaLoaiDauTu == maLoaiDauTu.Value);
            }

            var dauTus = await query
                .OrderBy(d => d.Ngay)
                .ToListAsync();

            // Chuẩn bị dữ liệu cho biểu đồ
            var labels = dauTus.Select(d => $"{d.LoaiDauTu?.TenLoai} ({d.Ngay.ToString("dd/MM/yyyy")})").ToList();
            var loiNhuanData = dauTus.Select(d => (double)(d.GiaTriHienTai - d.GiaTri)).ToList();
            var colors = loiNhuanData.Select(l => l >= 0 ? "rgba(75, 192, 192, 0.6)" : "rgba(255, 99, 132, 0.6)").ToList();

            ViewBag.Labels = System.Text.Json.JsonSerializer.Serialize(labels);
            ViewBag.LoiNhuanData = System.Text.Json.JsonSerializer.Serialize(loiNhuanData);
            ViewBag.Colors = System.Text.Json.JsonSerializer.Serialize(colors);

            // Truyền giá trị bộ lọc để hiển thị lại trong form
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.MaLoaiDauTu = maLoaiDauTu;

            // Truyền danh sách loại đầu tư cho dropdown
            ViewBag.LoaiDauTuList = new SelectList(await _context.LoaiDauTu.ToListAsync(), "MaLoaiDauTu", "TenLoai", maLoaiDauTu);

            return View(dauTus);
        }

        // GET: DauTu/Create
        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loaiDauTuList = _context.LoaiDauTu.ToList();

            if (!loaiDauTuList.Any())
            {
                TempData["ErrorMessage"] = "Vui lòng thêm loại đầu tư trước khi tạo đầu tư.";
                return RedirectToAction("Index", "LoaiDauTu"); // Giả sử bạn có LoaiDauTuController
            }

            ViewBag.LoaiDauTu = new SelectList(loaiDauTuList, "MaLoaiDauTu", "TenLoai");
            return View();
        }

        // POST: DauTu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiDauTu,GiaTri,GhiChu")] DauTu dauTu)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dauTu.MaNguoiDung = userId;
            dauTu.Ngay = DateTime.Now;
            dauTu.GiaTriHienTai = dauTu.GiaTri; // Giá trị hiện tại ban đầu bằng giá trị đầu tư
            dauTu.TrangThai = "HoatDong";

            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("Ngay");
            ModelState.Remove("GiaTriHienTai");
            ModelState.Remove("TrangThai");
            ModelState.Remove("NgayKetThuc");
            ModelState.Remove("NguoiDung");
            ModelState.Remove("LoaiDauTu");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.DauTu.Add(dauTu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo đầu tư: {ex.Message}");
                }
            }

            ViewBag.LoaiDauTu = new SelectList(_context.LoaiDauTu, "MaLoaiDauTu", "TenLoai", dauTu.MaLoaiDauTu);
            return View(dauTu);
        }

        // GET: DauTu/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dauTu = await _context.DauTu
                .FirstOrDefaultAsync(d => d.MaDauTu == id && d.MaNguoiDung == userId);

            if (dauTu == null)
            {
                return NotFound();
            }

            ViewBag.LoaiDauTu = new SelectList(_context.LoaiDauTu, "MaLoaiDauTu", "TenLoai", dauTu.MaLoaiDauTu);
            return View(dauTu);
        }

        // POST: DauTu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDauTu,MaLoaiDauTu,GiaTri,GiaTriHienTai,NgayKetThuc,GhiChu")] DauTu dauTu)
        {
            if (id != dauTu.MaDauTu)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingDauTu = await _context.DauTu
                .FirstOrDefaultAsync(d => d.MaDauTu == id && d.MaNguoiDung == userId);

            if (existingDauTu == null)
            {
                return NotFound();
            }

            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("Ngay");
            ModelState.Remove("TrangThai");
            ModelState.Remove("NguoiDung");
            ModelState.Remove("LoaiDauTu");

            if (ModelState.IsValid)
            {
                try
                {
                    existingDauTu.MaLoaiDauTu = dauTu.MaLoaiDauTu;
                    existingDauTu.GiaTri = dauTu.GiaTri;
                    existingDauTu.GiaTriHienTai = dauTu.GiaTriHienTai;
                    existingDauTu.NgayKetThuc = dauTu.NgayKetThuc;
                    existingDauTu.GhiChu = dauTu.GhiChu;

                    // Cập nhật trạng thái
                    if (dauTu.NgayKetThuc.HasValue)
                    {
                        existingDauTu.TrangThai = "DaBan";
                    }
                    else
                    {
                        existingDauTu.TrangThai = "HoatDong";
                    }

                    _context.DauTu.Update(existingDauTu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DauTu.Any(d => d.MaDauTu == dauTu.MaDauTu))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewBag.LoaiDauTu = new SelectList(_context.LoaiDauTu, "MaLoaiDauTu", "TenLoai", dauTu.MaLoaiDauTu);
            return View(dauTu);
        }

        // GET: DauTu/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dauTu = await _context.DauTu
                .Include(d => d.LoaiDauTu)
                .FirstOrDefaultAsync(d => d.MaDauTu == id && d.MaNguoiDung == userId);

            if (dauTu == null)
            {
                return NotFound();
            }

            return View(dauTu);
        }

        // POST: DauTu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dauTu = await _context.DauTu
                .FirstOrDefaultAsync(d => d.MaDauTu == id && d.MaNguoiDung == userId);

            if (dauTu != null)
            {
                _context.DauTu.Remove(dauTu);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}