using AI_Application.Models.SinhVien;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AI_Application.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Chat() => View();

        public IActionResult ExplainCode() => View();

        public IActionResult AskExercise() => View();

        public IActionResult ViewMaterials() => View();

        public IActionResult TrackProgress() => View();

        public IActionResult LookupInfo() => View();

        public IActionResult UploadDocument() => View();

        [HttpPost]
        public IActionResult UploadDocument(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadedFile.FileName);
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