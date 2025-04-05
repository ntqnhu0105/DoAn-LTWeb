using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class MucTieuController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MucTieuController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // GET: Goal/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var goals = await _context.MucTieu
                .Where(m => m.MaNguoiDung == userId)
                .Include(m => m.DanhMuc)
                .OrderBy(m => m.HanChot)
                .ToListAsync();

            // Cập nhật trạng thái cho từng mục tiêu
            foreach (var goal in goals)
            {
                if (goal.SoTienHienTai >= goal.SoTienMucTieu)
                {
                    goal.TrangThai = "HoanThanh";
                }
                else if (DateTime.Now > goal.HanChot && goal.SoTienHienTai < goal.SoTienMucTieu)
                {
                    goal.TrangThai = "ThatBai";
                }
                else
                {
                    goal.TrangThai = "DangTienHanh";
                }
            }

            await _context.SaveChangesAsync(); // Lưu thay đổi trạng thái vào database
            return View(goals);
        }

        // GET: Goal/Create
        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var danhMucList = _context.DanhMuc.ToList();

            if (!danhMucList.Any())
            {
                TempData["ErrorMessage"] = "Vui lòng thêm danh mục trước khi tạo mục tiêu.";
                return RedirectToAction("Index", "Category");
            }

            // Debug danh mục
            Console.WriteLine("Danh sách danh mục trong ViewBag:");
            foreach (var dm in danhMucList)
            {
                Console.WriteLine($"MaDanhMuc: {dm.MaDanhMuc}, TenDanhMuc: {dm.TenDanhMuc}");
            }

            ViewBag.DanhMuc = new SelectList(danhMucList, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Goal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenMucTieu,SoTienMucTieu,MaDanhMuc,HanChot,GhiChu")] MucTieu mucTieu)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            mucTieu.MaNguoiDung = userId;
            mucTieu.NgayTao = DateTime.Now;
            mucTieu.SoTienHienTai = 0;
            mucTieu.TrangThai = "DangTienHanh";

            // Xóa validation cho các trường không gửi từ form
            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("SoTienHienTai");
            ModelState.Remove("TrangThai");
            ModelState.Remove("NgayTao");
            ModelState.Remove("NguoiDung");
            ModelState.Remove("DanhMuc"); // Quan trọng: Loại bỏ validation cho navigation property

            // Debug dữ liệu gửi lên
            Console.WriteLine("Dữ liệu gửi từ form:");
            Console.WriteLine($"TenMucTieu: {mucTieu.TenMucTieu}");
            Console.WriteLine($"SoTienMucTieu: {mucTieu.SoTienMucTieu}");
            Console.WriteLine($"MaDanhMuc: {mucTieu.MaDanhMuc}");
            Console.WriteLine($"HanChot: {mucTieu.HanChot}");
            Console.WriteLine($"GhiChu: {mucTieu.GhiChu}");

            if (mucTieu.MaDanhMuc <= 0)
            {
                ModelState.AddModelError("MaDanhMuc", "Vui lòng chọn một danh mục hợp lệ.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine($"ModelState Error: {error}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.MucTieu.Add(mucTieu);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Mục tiêu đã được lưu thành công.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi lưu mục tiêu: {ex.Message}");
                    Console.WriteLine($"Exception: {ex}");
                }
            }

            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc", mucTieu.MaDanhMuc);
            return View(mucTieu);
        }

        // GET: Goal/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mucTieu = await _context.MucTieu
                .FirstOrDefaultAsync(m => m.MaMucTieu == id && m.MaNguoiDung == userId);

            if (mucTieu == null)
            {
                return NotFound();
            }

            // Debug giá trị
            Console.WriteLine($"MaMucTieu: {mucTieu.MaMucTieu}");
            Console.WriteLine($"TenMucTieu: {mucTieu.TenMucTieu}");
            Console.WriteLine($"SoTienMucTieu: {mucTieu.SoTienMucTieu}");
            Console.WriteLine($"SoTienHienTai: {mucTieu.SoTienHienTai}");
            Console.WriteLine($"MaDanhMuc: {mucTieu.MaDanhMuc}");
            Console.WriteLine($"HanChot: {mucTieu.HanChot}");

            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc", mucTieu.MaDanhMuc);
            return View(mucTieu);
        }

        // POST: Goal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMucTieu,TenMucTieu,SoTienMucTieu,SoTienHienTai,MaDanhMuc,HanChot,GhiChu")] MucTieu mucTieu)
        {
            if (id != mucTieu.MaMucTieu)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingGoal = await _context.MucTieu
                .FirstOrDefaultAsync(m => m.MaMucTieu == id && m.MaNguoiDung == userId);

            if (existingGoal == null)
            {
                return NotFound();
            }

            // Xóa validation cho các trường không gửi từ form
            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("NguoiDung");
            ModelState.Remove("DanhMuc");
            ModelState.Remove("TrangThai");
            ModelState.Remove("NgayTao");

            // Debug dữ liệu gửi từ form
            Console.WriteLine("Dữ liệu gửi từ form:");
            Console.WriteLine($"MaMucTieu: {mucTieu.MaMucTieu}");
            Console.WriteLine($"TenMucTieu: {mucTieu.TenMucTieu}");
            Console.WriteLine($"SoTienMucTieu: {mucTieu.SoTienMucTieu}");
            Console.WriteLine($"SoTienHienTai: {mucTieu.SoTienHienTai}");
            Console.WriteLine($"MaDanhMuc: {mucTieu.MaDanhMuc}");
            Console.WriteLine($"HanChot: {mucTieu.HanChot}");
            Console.WriteLine($"GhiChu: {mucTieu.GhiChu}");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine($"ModelState Error: {error}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Giữ nguyên các trường không thay đổi từ form
                    existingGoal.TenMucTieu = mucTieu.TenMucTieu;
                    existingGoal.SoTienMucTieu = mucTieu.SoTienMucTieu;
                    existingGoal.SoTienHienTai = mucTieu.SoTienHienTai;
                    existingGoal.MaDanhMuc = mucTieu.MaDanhMuc;
                    existingGoal.HanChot = mucTieu.HanChot;
                    existingGoal.GhiChu = mucTieu.GhiChu;

                    // Cập nhật trạng thái
                    if (existingGoal.SoTienHienTai >= existingGoal.SoTienMucTieu)
                    {
                        existingGoal.TrangThai = "HoanThanh";
                    }
                    else if (DateTime.Now > existingGoal.HanChot && existingGoal.SoTienHienTai < existingGoal.SoTienMucTieu)
                    {
                        existingGoal.TrangThai = "ThatBai";
                    }
                    else
                    {
                        existingGoal.TrangThai = "DangTienHanh";
                    }

                    _context.MucTieu.Update(existingGoal);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Mục tiêu đã được cập nhật thành công.");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MucTieu.Any(m => m.MaMucTieu == mucTieu.MaMucTieu))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewBag.DanhMuc = new SelectList(_context.DanhMuc, "MaDanhMuc", "TenDanhMuc", mucTieu.MaDanhMuc);
            return View(mucTieu);
        }

        // GET: Goal/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mucTieu = await _context.MucTieu
                .Include(m => m.DanhMuc)
                .FirstOrDefaultAsync(m => m.MaMucTieu == id && m.MaNguoiDung == userId);

            if (mucTieu == null)
            {
                return NotFound();
            }

            return View(mucTieu);
        }

        // POST: Goal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mucTieu = await _context.MucTieu
                .FirstOrDefaultAsync(m => m.MaMucTieu == id && m.MaNguoiDung == userId);

            if (mucTieu != null)
            {
                _context.MucTieu.Remove(mucTieu);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Progress()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mucTieu = await _context.MucTieu
                .Where(m => m.MaNguoiDung == userId && m.TrangThai == "DangTienHanh")
                .ToListAsync();

            var labels = mucTieu.Select(m => m.TenMucTieu).ToList();
            var tienDo = mucTieu.Select(m => m.SoTienMucTieu > 0 ? (double)(m.SoTienHienTai / m.SoTienMucTieu * 100) : 0).ToList();
            var colors = tienDo.Select(t => t >= 100 ? "rgba(75, 192, 192, 0.6)" : "rgba(255, 99, 132, 0.6)").ToList();

            ViewBag.Labels = System.Text.Json.JsonSerializer.Serialize(labels);
            ViewBag.TienDo = System.Text.Json.JsonSerializer.Serialize(tienDo);
            ViewBag.Colors = System.Text.Json.JsonSerializer.Serialize(colors);

            return View(mucTieu);
        }
    }
}