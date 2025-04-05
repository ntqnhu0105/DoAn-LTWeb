using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Threading.Tasks;

namespace QLTCCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NoKhoanVayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoKhoanVayController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var noKhoanVay = await _context.NoKhoanVay
                .Include(n => n.LichSuTraNos)
                .Include(n => n.NguoiDung)
                .OrderBy(n => n.NgayBatDau)
                .ToListAsync();

            return View(noKhoanVay);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noKhoanVay = await _context.NoKhoanVay
                .Include(n => n.LichSuTraNos)
                .Include(n => n.NguoiDung)
                .FirstOrDefaultAsync(n => n.MaNo == id);

            if (noKhoanVay == null)
            {
                return NotFound();
            }

            return View(noKhoanVay);
        }
    }
}