using System.Threading.Tasks;
using AI_Application.Data;
using AI_Application.Models;
using AI_Application.Models.Users;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult userlogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> userlogin(UserViewModel viewModel)
        {
            var user = new Users
            {
                Username = viewModel.Username,
                Password = viewModel.Password,
                Email = viewModel.Email,
                Role = viewModel.Role
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return View();
        }
    }
}