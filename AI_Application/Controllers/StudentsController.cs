using AI_Application.Models.SinhVien;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using AI_Application.Data;
using Microsoft.EntityFrameworkCore;
using AI_Application.Models;
using Microsoft.AspNetCore.Http;

namespace AI_Application.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();
        public IActionResult Chat() => View();

        // Giải thích code
        public IActionResult ExplainCode() => View();

        [HttpPost]
        public IActionResult ExplainCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                string explanation = $"Giải thích giả lập: đoạn code có {code.Length} ký tự.";
                ViewBag.Explanation = explanation;
            }
            return View();
        }

        // Gửi bài tập 
        public IActionResult AskExercise() => View();

        [HttpPost]
        public async Task<IActionResult> AskExercise(CauHoi model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                model.ThoiGianGui = DateTime.Now;
                model.NguoiGui = User.Identity?.Name ?? "SinhVien";
                model.DaDuyet = false;

                _context.CauHois.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Gửi câu hỏi thành công!";
                return RedirectToAction("AskExercise");
            }
            return View(model);
        }

        // Xem phản hồi từ giảng viên 
        public IActionResult ViewReplies()
        {
            var tenSinhVien = User.Identity?.Name ?? "SinhVien";

            var replies = _context.PhanHoiCauHois
                .Where(p => p.NguoiNhan == tenSinhVien && p.DaDuyet)
                .Include(p => p.CauHoi)
                .OrderByDescending(p => p.ThoiGianPhanHoi)
                .ToList();

            return View(replies);
        }

        // Xem tài liệu học tập 
        public IActionResult ViewMaterials()
        {
            var materialsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "materials");
            if (!Directory.Exists(materialsPath))
                Directory.CreateDirectory(materialsPath);

            var files = Directory.GetFiles(materialsPath)
                                 .Select(Path.GetFileName)
                                 .ToList();

            ViewBag.Materials = files;
            return View();
        }

        // Theo dõi tiến trình 
        public IActionResult TrackProgress()
        {
            var name = User.Identity?.Name ?? "SinhVien";

            var progress = new List<string>
            {
                "Đã hoàn thành: Bài tập tuần 1",
                "Đã xem: Tài liệu 'Lập trình OOP'",
                "Chưa hoàn thành: Bài trắc nghiệm số 2"
            };

            ViewBag.Progress = progress;
            return View();
        }

        // Tra cứu thông tin 
        public IActionResult LookupInfo() => View();

        [HttpPost]
        public IActionResult LookupInfo(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                ViewBag.KetQua = $"Thông tin tra cứu cho '{keyword}' là ... (mô phỏng)";
            }
            return View();
        }

        // Tải file lên 
        public IActionResult UploadDocument() => View();

        [HttpPost]
        public async Task<IActionResult> UploadDocument(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }

                    var document = new UploadedDocument
                    {
                        StudentId = HttpContext.Session.GetString("UserId") ?? "0",
                        FileName = uploadedFile.FileName,
                        FilePath = "/uploads/" + uniqueFileName,
                        UploadedAt = DateTime.Now
                    };

                    _context.UploadedDocuments.Add(document);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = $"Tải tệp '{uploadedFile.FileName}' lên thành công!";
                }
                catch (Exception ex)
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

        // Lưu tài liệu
        public IActionResult SaveDocument()
        {
            ViewData["Title"] = "Lưu tài liệu";
            return View();
        }

        [HttpPost]
        public IActionResult SaveDocument(string title, string content)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "saved");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Path.Combine(folder, $"{title}.txt");

            System.IO.File.WriteAllText(fileName, content);

            ViewBag.Message = $"Đã lưu tài liệu: {title}";
            return View();
        }
    }
}
