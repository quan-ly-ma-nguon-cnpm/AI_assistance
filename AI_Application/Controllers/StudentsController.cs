using AI_Application.Models.SinhVien;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using AI_Application.Data;
using Microsoft.EntityFrameworkCore;
using AI_Application.Models;

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

        // ✅ GIẢI THÍCH CODE
        public IActionResult ExplainCode() => View();

        [HttpPost]
        public async Task<IActionResult> ExplainCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                string explanation = $"Giải thích giả lập: đoạn code có {code.Length} ký tự.";
                ViewBag.Explanation = explanation;
            }
            return View();
        }

        // ✅ GỬI BÀI TẬP
        public IActionResult AskExercise() => View();

        [HttpPost]
        public IActionResult AskExercise(CauHoi model)
        {
            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                model.ThoiGianGui = DateTime.Now;
                model.NguoiGui = User.Identity?.Name ?? "SinhVien";
                model.DaDuyet = false;

                _context.CauHois.Add(model);
                _context.SaveChanges();

                TempData["Success"] = "Gửi câu hỏi thành công!";
                return RedirectToAction("AskExercise");
            }
            return View(model);
        }

        // ✅ XEM PHẢN HỒI TỪ GIẢNG VIÊN
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

        // ✅ XEM TÀI LIỆU HỌC TẬP
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

        // ✅ THEO DÕI TIẾN TRÌNH HỌC TẬP
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

        // ✅ TRA CỨU THÔNG TIN
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

        // ✅ TẢI FILE LÊN
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

        // ✅ LƯU TÀI LIỆU
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
