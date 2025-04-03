using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLTCCN.Models.Data;

namespace QLTCCN.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string email, string password, string fullName, string phoneNumber, string userName = null)
        {
            if (ModelState.IsValid)
            {
                // Nếu không truyền userName, có thể tạo mặc định (ví dụ: từ fullName hoặc email)
                var finalUserName = string.IsNullOrEmpty(userName) ? fullName.Replace(" ", "").ToLower() : userName;

                var user = new ApplicationUser
                {
                    UserName = finalUserName, // Gán UserName riêng biệt
                    FullName = fullName,
                    PhoneNumber = phoneNumber,
                    Email = email
                };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                // Tìm người dùng dựa trên email
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email không tồn tại.");
                    return View();
                }

                // Đăng nhập bằng UserName của người dùng
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Mật khẩu không đúng.");
            }
            return View();
        }

        // POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
