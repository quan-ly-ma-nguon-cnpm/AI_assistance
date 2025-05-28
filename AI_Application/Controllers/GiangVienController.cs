public class GiangVienController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult DanhSachCauHoi()
    {
        // lấy danh sách câu hỏi từ DB hoặc API
        return View();
    }

    public IActionResult DuyetPhanHoi()
    {
        // hiển thị danh sách phản hồi từ sinh viên
        return View();
    }

    public IActionResult CapNhatKienThuc()
    {
        // Form cập nhật tri thức mới
        return View();
    }
}
