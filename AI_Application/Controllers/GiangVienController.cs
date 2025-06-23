using Microsoft.AspNetCore.Mvc;
using AI_Application.Data; // ApplicationDbContext
using AI_Application.Models; // CauHoi, CauHoiViewModel
using System.Linq;

public class GiangVienController : Controller
{
    private readonly ApplicationDbContext _context;

    public GiangVienController(ApplicationDbContext context)
    {
        _context = context;
    }
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
#pragma warning disable CS8601 // Possible null reference assignment.
        List<CauHoiViewModel> DanhSachCauHoi = _context.CauHois.Select(static c => new CauHoiViewModel
        {
            Id = c.Id,
            TieuDe = c.TieuDe,
            LinhVuc = c.LinhVuc,
            NgayTao = c.NgayTao,
            NguoiGui = c.NguoiGui,
            DaDuyet = c.DaDuyet
        }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

        return View(DanhSachCauHoi);
    }

    public IActionResult DuyetPhanHoi()
    {
        
        List<PhanHoiViewModel> danhSachPhanHoi = _context.PhanHois!
            .Select(p => new PhanHoiViewModel 
            {
                Id = p.Id,
                NoiDung = p.NoiDung,
                TieuDe = p.TieuDe,
                LinhVuc = p.LinhVuc,
                NgayTao = p.NgayTao,
                NguoiGui = p.NguoiGui, 
                ThoiGianGui = p.ThoiGianGui,
                DaDuyet = p.DaDuyet
            })
            .ToList();

        // **Quan trọng:**
        // Nếu _context.PhanHois không có dữ liệu, .ToList() sẽ trả về một danh sách rỗng (empty list),
        // chứ không phải null. Vì vậy, bạn không cần phải kiểm tra null ở đây.

        return View(danhSachPhanHoi); // Truyền danh sách phản hồi vào View
    }

    // Bạn cũng sẽ cần một Post action để xử lý việc duyệt phản hồi
    [HttpPost]
    public IActionResult DuyetPhanHoi(int id)
    {
        var phanHoiToUpdate = _context.PhanHois.Find(id); // Tìm phản hồi theo ID

        if (phanHoiToUpdate != null)
        {
            phanHoiToUpdate.DaDuyet = true; // Đặt trạng thái đã duyệt
            _context.SaveChanges(); // Lưu thay đổi vào database
        }

        return RedirectToAction(nameof(DuyetPhanHoi)); // Chuyển hướng về trang danh sách để cập nhật hiển thị
    }
    
}