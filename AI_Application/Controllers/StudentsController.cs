using Microsoft.AspNetCore.Mvc;

namespace AI_Application.Controllers
{
    public class StudentController : Controller
    {
        // Trò chuyện với Hannah
        public IActionResult ChatWithHannah()
        {
            return View();
        }

        // Chia sẻ và giải thích code
        public IActionResult ShareAndExplainCode()
        {
            return View();
        }

        // Hỏi về bài tập
        public IActionResult AskAboutExercises()
        {
            return View();
        }

        // Xem tài liệu học tập
        public IActionResult ViewLearningMaterials()
        {
            return View();
        }

        // Theo dõi tiến độ học tập
        public IActionResult TrackProgress()
        {
            return View();
        }

        // Tra cứu thông tin
        public IActionResult LookupInformation()
        {
            return View();
        }

        // Lưu tài liệu
        public IActionResult SaveDocument()
        {
            return View();
        }

        // Tải tài liệu lên (GET)
        [HttpGet]
        public IActionResult UploadDocument()
        {
            return View();
        }

        // Tải tài liệu lên (POST)
        [HttpPost]
        public IActionResult UploadDocument(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                // Xử lý tệp được tải lên ở đây (lưu vào thư mục hoặc database)
                // Ví dụ (lưu tạm):
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", uploadedFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(stream);
                }
                ViewBag.Message = "Tải tệp lên thành công!";
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn tệp.";
            }

            return View();
        }
    }
}
