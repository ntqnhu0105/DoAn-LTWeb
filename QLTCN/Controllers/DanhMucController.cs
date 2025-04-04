using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Threading.Tasks;

namespace QLTCCN.Controllers
{
    [Authorize] // Yêu cầu đăng nhập để truy cập
    public class DanhMucController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DanhMucController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category/Index - Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var categories = await _context.DanhMuc
                .OrderBy(c => c.TenDanhMuc)
                .ToListAsync();
            return View(categories);
        }

        // GET: Category/Create - Hiển thị form thêm danh mục
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create - Thêm danh mục mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenDanhMuc,Loai")] DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = string.Join("; ", errors);
                return View(danhMuc);
            }

            _context.DanhMuc.Add(danhMuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Edit/5 - Hiển thị form sửa danh mục
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var danhMuc = await _context.DanhMuc.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        // POST: Category/Edit/5 - Cập nhật danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DanhMuc danhMuc)
        {
            if (id != danhMuc.MaDanhMuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.MaDanhMuc))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }

        // GET: Category/Delete/5 - Hiển thị xác nhận xóa danh mục
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var danhMuc = await _context.DanhMuc
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        // POST: Category/Delete/5 - Xóa danh mục
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMuc = await _context.DanhMuc.FindAsync(id);
            if (danhMuc != null)
            {
                // Kiểm tra xem danh mục có đang được sử dụng trong GiaoDich hoặc MucTieu không
                if (_context.GiaoDich.Any(g => g.MaDanhMuc == id) || _context.MucTieu.Any(m => m.MaDanhMuc == id))
                {
                    TempData["ErrorMessage"] = "Không thể xóa danh mục này vì nó đang được sử dụng.";
                    return RedirectToAction(nameof(Index));
                }

                _context.DanhMuc.Remove(danhMuc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucExists(int id)
        {
            return _context.DanhMuc.Any(e => e.MaDanhMuc == id);
        }
    }
}