using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;
using System.Threading.Tasks;

namespace QLTCCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GiaoDichController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GiaoDichController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var giaoDich = await _context.GiaoDich
                .Include(g => g.DanhMuc)
                .Include(g => g.TaiKhoan)
                .Include(g => g.NguoiDung)
                .OrderBy(g => g.NgayGiaoDich)
                .ToListAsync();

            return View(giaoDich);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giaoDich = await _context.GiaoDich
                .Include(g => g.DanhMuc)
                .Include(g => g.TaiKhoan)
                .Include(g => g.NguoiDung)
                .FirstOrDefaultAsync(g => g.MaGiaoDich == id);

            if (giaoDich == null)
            {
                return NotFound();
            }

            return View(giaoDich);
        }
    }
}