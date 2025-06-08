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
        var danhSachCauHoi = _context.CauHois.Select(static c => new CauHoiViewModel
        {
            Id = c.Id,
            TieuDe = c.TieuDe,
            LinhVuc = c.LinhVuc,
            NgayTao = c.NgayTao,
            NguoiGui = c.NguoiGui,
            DaDuyet = c.DaDuyet
        }).ToList();

        return View(danhSachCauHoi);
    }

    public IActionResult DuyetPhanHoi()
    {
        return View();
    }
}
