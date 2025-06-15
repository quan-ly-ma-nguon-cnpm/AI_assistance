using System.Threading.Tasks;
using AI_Application.Data;
using AI_Application.Models;
using AI_Application.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Core.Types;
namespace AI_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult userRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserViewModel viewModel)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == viewModel.Username || u.Email == viewModel.Email);

            if (existingUser != null)
            {
                ViewBag.register_message = "Tài khoản đã tồn tại hoặc email đã được sử dụng !";
                return View();
            }

            var user = new Users
            {
                Username = viewModel.Username,
                Password = "",
                Email = viewModel.Email,
                Role = string.IsNullOrEmpty(viewModel.Role) ? "Student" : viewModel.Role,
            };

            var passwordHasher = new PasswordHasher<Users>();
            user.Password = passwordHasher.HashPassword(user, viewModel.Password);

            try
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                ViewBag.register_message = "Đăng ký thành công !";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi hệ thống: " + ex.Message);
                ModelState.AddModelError(string.Empty, "Đăng ký thất bại do lỗi hệ thống.");
            }

            return View();
        }

        [HttpGet]
        public IActionResult userLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> userLogin(UserViewModel viewModel)
        {

            var user = await dbContext.Users
            .Where(u => u.Email == viewModel.Email || u.Username == viewModel.Username)
            .FirstOrDefaultAsync();
            if (user != null && user.Role == "Student")
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                return Redirect("/Students/Index");
            }
            else if (user != null && user.Role == "Faculty")
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                return Redirect("/GiangVien/Index");
            }

            ViewBag.register_message = "Không thể đăng nhập";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var user = await dbContext.Users
            .Where(u => u.Username == HttpContext.Session.GetString("Username"))
            .FirstOrDefaultAsync();

            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserId");

            TempData["LogoutMessage"] = "Bạn đã đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }
    }
}