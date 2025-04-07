using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLTCCN.Models.Data;

namespace QLTCCN.Services
{
    public class SurvivalModeService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SurvivalModeService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CheckSurvivalModeAsync(string userId, decimal threshold = 500000)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            // Tổng số dư các tài khoản của người dùng
            var tongSoDu = await _context.TaiKhoan
                .Where(t => t.MaNguoiDung == userId)
                .SumAsync(t => t.SoDu);

            if (tongSoDu < threshold && !user.SurvivalMode)
            {
                user.SurvivalMode = true;
                user.SurvivalModeStartDate = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }
            else if (tongSoDu >= threshold && user.SurvivalMode)
            {
                user.SurvivalMode = false;
                user.SurvivalModeStartDate = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }

}
