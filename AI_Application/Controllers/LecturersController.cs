using Microsoft.AspNetCore.Mvc;
using AI_Application.Data;
using AI_Application.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using System;

public class LecturersController : Controller
{
    private readonly ApplicationDbContext _context;

    public LecturersController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult XemCauHoi()
    {
        var cauHois = _context.CauHois
        .Include(c => c.PhanHois)
        .OrderByDescending(c => c.ThoiGianGui)
        .ToList();
        return View(cauHois);
    }

    public IActionResult GuiPhanHoi(int cauHoiId)
    {
        var cauHoi = _context.CauHois.FirstOrDefault(c => c.Id == cauHoiId);
        if (cauHoi == null) return NotFound();
        ViewBag.CauHoi = cauHoi;
        return View();
    }
    [HttpPost]
    public IActionResult GuiPhanHoi(int cauHoiId, string noiDung)
    {
        var cauHoi = _context.CauHois.FirstOrDefault(c => c.Id == cauHoiId);
        if (cauHoi == null) return NotFound();
        var phanHoi = new PhanHoiCauHoi
        {
            NoiDung = noiDung,
            NguoiGui = User.Identity?.Name ?? "GV",
            NguoiNhan = cauHoi.NguoiGui,
            CauHoiId = cauHoi.Id,
            DaDuyet = true
        };
        _context.PhanHoiCauHois.Add(phanHoi);
        _context.SaveChanges();
        return RedirectToAction("XemCauHoi");
    }

    public IActionResult DuyetPhanHoi()
    {
        var danhSach = _context.PhanHoiCauHois
            .Where(p => !p.DaDuyet)
            .OrderByDescending(p => p.ThoiGianPhanHoi)
            .ToList();

        return View(danhSach);
    }

    [HttpPost]
    public IActionResult Duyet(int id)
    {
        var ph = _context.PhanHoiCauHois.FirstOrDefault(p => p.Id == id);
        if (ph != null)
        {
            ph.DaDuyet = true;
            _context.SaveChanges();
        }

        return RedirectToAction("DuyetPhanHoi");
    }
}
