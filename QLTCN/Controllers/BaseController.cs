using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLTCCN.Models.Data;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected async Task CheckAndCreateNotifications()
        {
            var controller = new ThongBaoController(_context)
            {
                ControllerContext = ControllerContext
            };
            await controller.CheckAndCreateNotifications();
        }
    }
}
