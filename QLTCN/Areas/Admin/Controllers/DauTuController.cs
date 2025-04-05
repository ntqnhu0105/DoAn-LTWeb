using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Threading.Tasks;

namespace QLTCCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DauTuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DauTuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int? maLoaiDauTu)
        {
            var query = _context.DauTu
                .Include(d => d.LoaiDauTu)
                .Include(d => d.NguoiDung)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(d => d.Ngay >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(d => d.Ngay <= endDate.Value);
            }
            if (maLoaiDauTu.HasValue)
            {
                query = query.Where(d => d.MaLoaiDauTu == maLoaiDauTu.Value);
            }

            var dauTus = await query.OrderBy(d => d.Ngay).ToListAsync();

            var labels = dauTus.Select(d => $"{d.LoaiDauTu?.TenLoai} ({d.Ngay.ToString("dd/MM/yyyy")})").ToList();
            var loiNhuanData = dauTus.Select(d => (double)(d.GiaTriHienTai - d.GiaTri)).ToList();
            var colors = loiNhuanData.Select(l => l >= 0 ? "rgba(75, 192, 192, 0.6)" : "rgba(255, 99, 132, 0.6)").ToList();

            ViewBag.Labels = System.Text.Json.JsonSerializer.Serialize(labels);
            ViewBag.LoiNhuanData = System.Text.Json.JsonSerializer.Serialize(loiNhuanData);
            ViewBag.Colors = System.Text.Json.JsonSerializer.Serialize(colors);

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.MaLoaiDauTu = maLoaiDauTu;
            ViewBag.LoaiDauTuList = new SelectList(await _context.LoaiDauTu.ToListAsync(), "MaLoaiDauTu", "TenLoai", maLoaiDauTu);

            return View(dauTus);
        }
    }
}