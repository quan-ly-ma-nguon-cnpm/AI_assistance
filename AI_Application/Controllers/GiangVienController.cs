using Microsoft.AspNetCore.Mvc;
using AI_Application.Data; // ApplicationDbContext
using AI_Application.Models; // CauHoi, CauHoiViewModel
using System.Linq;
using Microsoft.EntityFrameworkCore;


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

    public IActionResult DanhSachCauHoiChuaPhanHoi()
    {
        var cauHois = _context.CauHois
            .Where(c => !_context.PhanHoiCauHois.Any(p => p.CauHoiId == c.Id))
            .ToList();

        return View("DanhSachCauHoi", cauHois); // Dùng cùng View "DanhSachCauHoi"
    }

    public IActionResult DanhSachCauHoi()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        List<CauHoiViewModel> danhSach = _context.CauHois.Select(static c => new CauHoiViewModel
        {
            Id = c.Id,
            TieuDe = c.TieuDe,
            LinhVuc = c.LinhVuc,
            NgayTao = c.NgayTao,
            NguoiGui = c.NguoiGui,
            DaDuyet = c.DaDuyet
        }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

        return View(danhSach);
    }

    public IActionResult DuyetPhanHoi()
    {
        var danhSachPhanHoi = _context.PhanHoiCauHois
        .Include(p => p.CauHoi)
            .Select(static p => new PhanHoiViewModel
            {
                Id = p.Id,
                NoiDung = p.NoiDung,
                TieuDe = p.CauHoi.TieuDe,   
                LinhVuc = p.CauHoi.LinhVuc,
                NgayTao = p.CauHoi.NgayTao,
                NguoiGui = p.NguoiGui,
                ThoiGianGui = p.ThoiGianPhanHoi,
                DaDuyet = p.DaDuyet,
                ThoiGianDat = p.ThoiGianPhanHoi
            })
            .ToList();

        return View(danhSachPhanHoi);
    }

    [HttpPost]
    public IActionResult DuyetPhanHoi(int id)
    {
        var phanHoiToUpdate = _context.PhanHoiCauHois.Find(id);

        if (phanHoiToUpdate != null)
        {
            phanHoiToUpdate.DaDuyet = true;
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(DuyetPhanHoi));
    }

    [HttpPost]
    public IActionResult GuiPhanHoi(int cauHoiId, string noiDung)
    {
        var cauHoi = _context.CauHois.FirstOrDefault(c => c.Id == cauHoiId);
        if (cauHoi == null) return NotFound();

        var ph = new PhanHoiCauHoi
        {
            CauHoiId = cauHoiId,
            NoiDung = noiDung,
            NguoiGui = User.Identity?.Name ?? "GiangVien",
            NguoiNhan = cauHoi.NguoiGui,
            ThoiGianPhanHoi = DateTime.Now,
            DaDuyet = true
        };

        _context.PhanHoiCauHois.Add(ph);
        _context.SaveChanges();

        return RedirectToAction("DanhSachCauHoiChuaPhanHoi");
    }
}
