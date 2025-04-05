using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;

namespace QLTCCN.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MucTieuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MucTieuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MucTieu
        public async Task<IActionResult> Index()
        {
            var mucTieu = await _context.MucTieu
                .Include(m => m.DanhMuc)
                .Include(m => m.NguoiDung)
                .OrderBy(m => m.HanChot)
                .ToListAsync();

            return View(mucTieu);
        }

        // GET: Admin/MucTieu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mucTieu = await _context.MucTieu
                .Include(m => m.DanhMuc)
                .Include(m => m.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaMucTieu == id);

            if (mucTieu == null)
            {
                return NotFound();
            }

            return View(mucTieu);
        }

        // Các action khác (Edit, Delete) nếu cần
    }
}
