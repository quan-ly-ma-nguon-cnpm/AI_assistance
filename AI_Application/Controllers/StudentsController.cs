using AI_Application.Models.SinhVien;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AI_Application.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Chat() => View();

        public IActionResult ExplainCode() => View();

        public IActionResult AskExercise() => View();

        public IActionResult ViewMaterials() => View();

        public IActionResult TrackProgress() => View();

        public IActionResult LookupInfo() => View();

        public IActionResult UploadDocument() => View();

        [HttpPost]
        public async Task<IActionResult> UploadDocument(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }
                    ViewBag.Message = $"Tải tệp '{uploadedFile.FileName}' lên thành công!";
                }
                catch (System.Exception ex)
                {
                    ViewBag.Message = $"Lỗi khi tải tệp lên: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn tệp.";
            }

            return View();
        }

        public IActionResult SaveDocument()
        {
            ViewData["Title"] = "Lưu tài liệu";
            return View();
        }
    }
}