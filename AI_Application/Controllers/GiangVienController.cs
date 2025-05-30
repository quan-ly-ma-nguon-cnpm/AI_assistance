using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AI_Application.Controllers
{
    public class GiangVienController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CapNhatKienThuc()
        {
            return View();
        }
        public IActionResult DanhSachCauHoi()
        {
            return View();
        }
        public IActionResult DuyetPhanHoi()
        {
            return View();
        }
    }
}
