using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Threading.Tasks;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class LoaiDauTuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiDauTuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoaiDauTu/Index
        public async Task<IActionResult> Index()
        {
            var loaiDauTus = await _context.LoaiDauTu
                .OrderBy(l => l.TenLoai)
                .ToListAsync();
            return View(loaiDauTus);
        }

        // GET: LoaiDauTu/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiDauTu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenLoai")] LoaiDauTu loaiDauTu)
        {
            ModelState.Remove("DauTus");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.LoaiDauTu.Add(loaiDauTu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo loại đầu tư: {ex.Message}");
                }
            }
            return View(loaiDauTu);
        }

        // GET: LoaiDauTu/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var loaiDauTu = await _context.LoaiDauTu
                .FirstOrDefaultAsync(l => l.MaLoaiDauTu == id);

            if (loaiDauTu == null)
            {
                return NotFound();
            }

            return View(loaiDauTu);
        }

        // POST: LoaiDauTu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiDauTu,TenLoai")] LoaiDauTu loaiDauTu)
        {
            if (id != loaiDauTu.MaLoaiDauTu)
            {
                return NotFound();
            }

            ModelState.Remove("DauTus");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.LoaiDauTu.Update(loaiDauTu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LoaiDauTu.Any(l => l.MaLoaiDauTu == loaiDauTu.MaLoaiDauTu))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật loại đầu tư: {ex.Message}");
                }
            }
            return View(loaiDauTu);
        }

        // GET: LoaiDauTu/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var loaiDauTu = await _context.LoaiDauTu
                .FirstOrDefaultAsync(l => l.MaLoaiDauTu == id);

            if (loaiDauTu == null)
            {
                return NotFound();
            }

            return View(loaiDauTu);
        }

        // POST: LoaiDauTu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiDauTu = await _context.LoaiDauTu
                .Include(l => l.DauTus)
                .FirstOrDefaultAsync(l => l.MaLoaiDauTu == id);

            if (loaiDauTu == null)
            {
                return NotFound();
            }

            if (loaiDauTu.DauTus != null && loaiDauTu.DauTus.Any())
            {
                ModelState.AddModelError("", "Không thể xóa loại đầu tư này vì đã có khoản đầu tư liên quan.");
                return View(loaiDauTu);
            }

            _context.LoaiDauTu.Remove(loaiDauTu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}