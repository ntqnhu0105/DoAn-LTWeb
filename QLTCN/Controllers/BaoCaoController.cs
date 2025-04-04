using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QLTCCN.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BaoCaoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BaoCao/Index - Hiển thị danh sách báo cáo
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var baoCaos = await _context.BaoCao
                .Where(b => b.MaNguoiDung == userId)
                .OrderBy(b => b.Nam)
                .ThenBy(b => b.Thang)
                .ThenBy(b => b.NgayTao)
                .ToListAsync();
            return View(baoCaos);
        }

        // GET: BaoCao/Report - Hiển thị báo cáo và biểu đồ với bộ lọc
        public async Task<IActionResult> Report(int? day, int? month, int? year)
        {
            var userId = _userManager.GetUserId(User);
            var query = _context.BaoCao.Where(b => b.MaNguoiDung == userId);

            // Lọc dữ liệu
            if (day.HasValue)
            {
                query = query.Where(b => b.NgayTao.Day == day.Value);
            }
            if (month.HasValue)
            {
                query = query.Where(b => b.Thang == month.Value);
            }
            if (year.HasValue)
            {
                query = query.Where(b => b.Nam == year.Value);
            }

            var baoCaos = await query.OrderBy(b => b.NgayTao).ToListAsync();

            // Kiểm tra dữ liệu
            if (!baoCaos.Any())
            {
                ViewBag.Message = "Không có dữ liệu báo cáo phù hợp với bộ lọc.";
            }

            // Chuẩn bị dữ liệu cho biểu đồ
            var chartData = new
            {
                Labels = baoCaos.Any() ? baoCaos.Select(b => $"{b.NgayTao:dd/MM/yyyy} (Tháng {b.Thang})").ToArray() : new string[] { "Không có dữ liệu" },
                ThuNhap = baoCaos.Any() ? baoCaos.Select(b => b.TongThuNhap).ToArray() : new decimal[] { 0 },
                ChiTieu = baoCaos.Any() ? baoCaos.Select(b => b.TongChiTieu).ToArray() : new decimal[] { 0 },
                TietKiem = baoCaos.Any() ? baoCaos.Select(b => b.SoTienTietKiem).ToArray() : new decimal[] { 0 }
            };

            ViewBag.ChartData = System.Text.Json.JsonSerializer.Serialize(chartData);
            ViewBag.Day = day;
            ViewBag.Month = month;
            ViewBag.Year = year;

            return View(baoCaos);
        }
    }
}