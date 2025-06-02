using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AI_Application.Controllers
{
    public class ChatbotController : Controller
    {
        public IActionResult Chatbot()
        {
            return View();
        }
    }
}