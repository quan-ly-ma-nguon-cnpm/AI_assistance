using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AI_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return Json(new { success = false });
            }

            string botResponse = GetBotResponse(message);
            _logger.LogInformation("User message: {Message}, Bot response: {Response}", message, botResponse);

            return Json(new { success = true, botMessage = botResponse });
        }

        private string GetBotResponse(string message)
        {
            message = message.ToLower();
            if (message.Contains("xin chào") || message.Contains("chào"))
            {
                return "Chào bạn! Rất vui được gặp bạn. Bạn khỏe không?";
            }
            else if (message.Contains("tôi khỏe") || message.Contains("khỏe"))
            {
                return "Tốt quá! Bạn cần tôi giúp gì hôm nay?";
            }
            else if (message.Contains("cảm ơn") || message.Contains("thanks"))
            {
                return "Không có gì! Mình luôn sẵn sàng giúp bạn.";
            }
            else
            {
                return "Mình chưa hiểu lắm, bạn có thể nói rõ hơn không?";
            }
        }
    }
}