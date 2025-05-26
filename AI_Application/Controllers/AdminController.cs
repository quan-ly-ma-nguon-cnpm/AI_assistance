using System.Diagnostics;
using AI_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AI_Application.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
