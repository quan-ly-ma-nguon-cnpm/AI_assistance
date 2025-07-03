using Microsoft.AspNetCore.Mvc;
using AI_Application.Data;
using AI_Application.Models;
using System.Linq;

public class SystemConfigController : Controller
{
    private readonly ApplicationDbContext _context;

    public SystemConfigController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var configs = _context.SystemConfigs.ToList();
        return View(configs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(SystemConfig config)
    {
        if (ModelState.IsValid)
        {
            config.UpdatedAt = DateTime.Now;
            _context.SystemConfigs.Add(config);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(config);
    }

    public IActionResult Edit(int id)
    {
        var config = _context.SystemConfigs.Find(id);
        return View(config);
    }

    [HttpPost]
    public IActionResult Edit(SystemConfig config)
    {
        if (ModelState.IsValid)
        {
            config.UpdatedAt = DateTime.Now;
            _context.SystemConfigs.Update(config);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(config);
    }

    public IActionResult Delete(int id)
    {
        var config = _context.SystemConfigs.Find(id);
        if (config != null)
        {
            _context.SystemConfigs.Remove(config);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
