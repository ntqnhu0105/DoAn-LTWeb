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
    public class NoKhoanVayController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public NoKhoanVayController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // GET: NoKhoanVay/Index
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVays = await _context.NoKhoanVay
                .Where(n => n.MaNguoiDung == userId)
                .OrderBy(n => n.NgayBatDau)
                .ToListAsync();

            // Cập nhật trạng thái
            foreach (var no in noKhoanVays)
            {
                var tongTienTra = await _context.LichSuTraNo
                    .Where(ls => ls.MaNo == no.MaNo)
                    .SumAsync(ls => ls.SoTienTra);

                if (tongTienTra >= no.SoTien * (1 + no.LaiSuat / 100))
                {
                    no.TrangThai = "DaThanhToan";
                    no.NgayKetThuc = DateTime.Now;
                }
                else if (DateTime.Now > no.NgayBatDau.AddMonths(no.KyHan) && tongTienTra < no.SoTien)
                {
                    no.TrangThai = "QuaHan";
                }
                else
                {
                    no.TrangThai = "HoatDong";
                }
            }

            await _context.SaveChangesAsync();
            return View(noKhoanVays);
        }

        // GET: NoKhoanVay/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: NoKhoanVay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoTien,LaiSuat,KyHan,NgayBatDau,GhiChu")] NoKhoanVay noKhoanVay)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            noKhoanVay.MaNguoiDung = userId;
            noKhoanVay.TrangThai = "HoatDong";
            noKhoanVay.NgayTraTiepTheo = noKhoanVay.NgayBatDau.AddMonths(1);

            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("TrangThai");
            ModelState.Remove("NgayKetThuc");
            ModelState.Remove("NgayTraTiepTheo");
            ModelState.Remove("NguoiDung");
            ModelState.Remove("LichSuTraNos");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.NoKhoanVay.Add(noKhoanVay);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi tạo khoản nợ: {ex.Message}");
                }
            }
            return View(noKhoanVay);
        }

        // GET: NoKhoanVay/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            return View(noKhoanVay);
        }

        // POST: NoKhoanVay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNo,SoTien,LaiSuat,KyHan,NgayBatDau,GhiChu")] NoKhoanVay noKhoanVay)
        {
            if (id != noKhoanVay.MaNo)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingNo = await _context.NoKhoanVay
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (existingNo == null)
            {
                return NotFound();
            }

            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("TrangThai");
            ModelState.Remove("NgayKetThuc");
            ModelState.Remove("NgayTraTiepTheo");
            ModelState.Remove("NguoiDung");
            ModelState.Remove("LichSuTraNos");

            if (ModelState.IsValid)
            {
                try
                {
                    existingNo.SoTien = noKhoanVay.SoTien;
                    existingNo.LaiSuat = noKhoanVay.LaiSuat;
                    existingNo.KyHan = noKhoanVay.KyHan;
                    existingNo.NgayBatDau = noKhoanVay.NgayBatDau;
                    existingNo.GhiChu = noKhoanVay.GhiChu;

                    _context.NoKhoanVay.Update(existingNo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.NoKhoanVay.Any(n => n.MaNo == noKhoanVay.MaNo))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(noKhoanVay);
        }

        // GET: NoKhoanVay/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            return View(noKhoanVay);
        }

        // POST: NoKhoanVay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay != null)
            {
                _context.NoKhoanVay.Remove(noKhoanVay);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: NoKhoanVay/LichSuTraNo/5
        [HttpGet]
        public async Task<IActionResult> LichSuTraNo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .Include(n => n.LichSuTraNos)
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            return View(noKhoanVay);
        }

        // GET: NoKhoanVay/ThemTraNo/5
        [HttpGet]
        public async Task<IActionResult> ThemTraNo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            var lichSuTraNo = new LichSuTraNo
            {
                MaNo = id,
                NgayTra = DateTime.Now
            };
            return View(lichSuTraNo);
        }

        // POST: NoKhoanVay/ThemTraNo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemTraNo(int id, [Bind("MaNo,SoTienTra,NgayTra,GhiChu")] LichSuTraNo lichSuTraNo)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            ModelState.Remove("NoKhoanVay");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.LichSuTraNo.Add(lichSuTraNo);
                    await _context.SaveChangesAsync();

                    // Cập nhật trạng thái và ngày trả tiếp theo
                    var tongTienTra = await _context.LichSuTraNo
                        .Where(ls => ls.MaNo == id)
                        .SumAsync(ls => ls.SoTienTra);

                    if (tongTienTra >= noKhoanVay.SoTien * (1 + noKhoanVay.LaiSuat / 100))
                    {
                        noKhoanVay.TrangThai = "DaThanhToan";
                        noKhoanVay.NgayKetThuc = DateTime.Now;
                        noKhoanVay.NgayTraTiepTheo = null;
                    }
                    else
                    {
                        noKhoanVay.NgayTraTiepTheo = lichSuTraNo.NgayTra.AddMonths(1);
                    }

                    _context.NoKhoanVay.Update(noKhoanVay);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(LichSuTraNo), new { id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi thêm lịch sử trả nợ: {ex.Message}");
                }
            }
            return View(lichSuTraNo);
        }
        // GET: NoKhoanVay/ChiTiet/5
        [HttpGet]
        public async Task<IActionResult> ChiTiet(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noKhoanVay = await _context.NoKhoanVay
                .Include(n => n.LichSuTraNos)
                .FirstOrDefaultAsync(n => n.MaNo == id && n.MaNguoiDung == userId);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            // Tính tổng tiền phải trả (bao gồm lãi)
            decimal tongTienPhaiTra = noKhoanVay.SoTien * (1 + noKhoanVay.LaiSuat / 100);

            // Tính số tiền trả mỗi tháng
            decimal tienTraMoiThang = tongTienPhaiTra / noKhoanVay.KyHan;

            // Tính tổng tiền đã trả
            decimal tongTienDaTra = noKhoanVay.LichSuTraNos?.Sum(ls => ls.SoTienTra) ?? 0;

            // Cập nhật trạng thái
            if (tongTienDaTra >= tongTienPhaiTra)
            {
                noKhoanVay.TrangThai = "DaThanhToan";
                noKhoanVay.NgayKetThuc = DateTime.Now;
                noKhoanVay.NgayTraTiepTheo = null;
            }
            else if (DateTime.Now > noKhoanVay.NgayBatDau.AddMonths(noKhoanVay.KyHan) && tongTienDaTra < noKhoanVay.SoTien)
            {
                noKhoanVay.TrangThai = "QuaHan";
            }
            else
            {
                noKhoanVay.TrangThai = "HoatDong";
            }

            await _context.SaveChangesAsync();

            // Truyền dữ liệu tính toán vào ViewBag
            ViewBag.TongTienPhaiTra = tongTienPhaiTra;
            ViewBag.TienTraMoiThang = tienTraMoiThang;
            ViewBag.TongTienDaTra = tongTienDaTra;
            ViewBag.ConLaiPhaiTra = tongTienPhaiTra - tongTienDaTra;

            return View(noKhoanVay);
        }
    }
}