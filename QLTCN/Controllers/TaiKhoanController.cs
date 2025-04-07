using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLTCCN.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QLTCCN.Services;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class TaiKhoanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SurvivalModeService _survivalModeService;

        public TaiKhoanController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SurvivalModeService survivalModeService)
        {
            _context = context;
            _userManager = userManager;
            _survivalModeService = survivalModeService;
        }

        // GET: TaiKhoan/Index - Hiển thị danh sách tài khoản
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var taiKhoans = await _context.TaiKhoan
                .Where(t => t.MaNguoiDung == userId)
                .ToListAsync();
            await _survivalModeService.CheckSurvivalModeAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.SurvivalMode = user?.SurvivalMode ?? false;

            return View(taiKhoans);
        }

        // GET: TaiKhoan/Create - Hiển thị form thêm tài khoản
        [HttpGet]
        public IActionResult Create()
        {
            // Dropdown cho LoaiTaiKhoan
            ViewBag.LoaiTaiKhoan = new SelectList(
                new[]
                {
                    new { Value = "Tiền mặt", Text = "Tiền mặt" },
                    new { Value = "Thẻ tín dụng", Text = "Thẻ tín dụng" },
                    new { Value = "Ngân hàng", Text = "Ngân hàng" },
                    new { Value = "Ví điện tử", Text = "Ví điện tử" }
                },
                "Value", "Text"
            );

            return View();
        }

        // POST: TaiKhoan/Create - Thêm tài khoản mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenTaiKhoan,SoDu,LoaiTaiKhoan")] TaiKhoan taiKhoan)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            taiKhoan.MaNguoiDung = userId; // Gán trực tiếp
            taiKhoan.NgayTao = DateTime.Now;

            // Xóa validation của MaNguoiDung khỏi ModelState
            ModelState.Remove("MaNguoiDung");

            Console.WriteLine($"UserId: {userId}");
            Console.WriteLine($"MaNguoiDung: {taiKhoan.MaNguoiDung}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TaiKhoan.Add(taiKhoan);
                    await _context.SaveChangesAsync();
                    await _survivalModeService.CheckSurvivalModeAsync(userId);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi lưu tài khoản: {ex.Message}");
                }
            }

            // Hiển thị lỗi nếu có
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
                Console.WriteLine($"ModelState Error: {error}");
            }

            ViewBag.LoaiTaiKhoan = new SelectList(
                new[]
                {
                    new { Value = "Tiền mặt", Text = "Tiền mặt" },
                    new { Value = "Thẻ tín dụng", Text = "Thẻ tín dụng" },
                    new { Value = "Ngân hàng", Text = "Ngân hàng" },
                    new { Value = "Ví điện tử", Text = "Ví điện tử" }
                },
                "Value", "Text", taiKhoan.LoaiTaiKhoan
            );

            return View(taiKhoan);
        }

        // GET: TaiKhoan/Edit/5 - Hiển thị form sửa tài khoản
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng
            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == id && t.MaNguoiDung == userId);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            // Gán lại MaNguoiDung từ userId nếu cần
            taiKhoan.MaNguoiDung = userId;

            // Cập nhật danh sách loại tài khoản cho dropdown
            ViewBag.LoaiTaiKhoan = new SelectList(
                new[]
                {
            new { Value = "Tiền mặt", Text = "Tiền mặt" },
            new { Value = "Thẻ tín dụng", Text = "Thẻ tín dụng" },
            new { Value = "Ngân hàng", Text = "Ngân hàng" },
            new { Value = "Ví điện tử", Text = "Ví điện tử" }
                },
                "Value",
                "Text",
                taiKhoan.LoaiTaiKhoan
            );

            return View(taiKhoan);
        }


        // POST: TaiKhoan/Edit/5 - Cập nhật tài khoản
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.MaTaiKhoan)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng
            taiKhoan.MaNguoiDung = userId; // Gán MaNguoiDung từ userId

            if (ModelState.IsValid)
            {
                var existingTaiKhoan = await _context.TaiKhoan
                    .FirstOrDefaultAsync(t => t.MaTaiKhoan == id && t.MaNguoiDung == userId);

                if (existingTaiKhoan == null)
                {
                    return NotFound();
                }

                // Cập nhật các trường cần thiết
                existingTaiKhoan.TenTaiKhoan = taiKhoan.TenTaiKhoan;
                existingTaiKhoan.SoDu = taiKhoan.SoDu;
                existingTaiKhoan.LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan;

                _context.TaiKhoan.Update(existingTaiKhoan);
                await _context.SaveChangesAsync();
                await _survivalModeService.CheckSurvivalModeAsync(userId);
                return RedirectToAction(nameof(Index));
            }

            // Cập nhật lại dropdown LoaiTaiKhoan nếu có lỗi
            ViewBag.LoaiTaiKhoan = new SelectList(
                new[]
                {
            new { Value = "Tiền mặt", Text = "Tiền mặt" },
            new { Value = "Thẻ tín dụng", Text = "Thẻ tín dụng" },
            new { Value = "Ngân hàng", Text = "Ngân hàng" },
            new { Value = "Ví điện tử", Text = "Ví điện tử" }
                },
                "Value",
                "Text",
                taiKhoan.LoaiTaiKhoan
            );

            return View(taiKhoan);
        }


        // GET: TaiKhoan/Delete/5 - Hiển thị xác nhận xóa tài khoản
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == id && t.MaNguoiDung == userId);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5 - Xóa tài khoản
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == id && t.MaNguoiDung == userId);

            if (taiKhoan != null)
            {
                _context.TaiKhoan.Remove(taiKhoan);
                await _context.SaveChangesAsync();
                await _survivalModeService.CheckSurvivalModeAsync(userId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
