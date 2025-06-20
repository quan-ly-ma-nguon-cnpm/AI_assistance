using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AI_Application.Data;
using AI_Application.Models.SinhVien;

namespace AI_Application.Controllers
{
    public class SavedDocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SavedDocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SavedDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.SavedDocuments.ToListAsync());
        }

        // GET: SavedDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedDocument = await _context.SavedDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedDocument == null)
            {
                return NotFound();
            }

            return View(savedDocument);
        }

        // GET: SavedDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SavedDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,FileName,FilePath,SavedAt")] SavedDocument savedDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savedDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(savedDocument);
        }

        // GET: SavedDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedDocument = await _context.SavedDocuments.FindAsync(id);
            if (savedDocument == null)
            {
                return NotFound();
            }
            return View(savedDocument);
        }

        // POST: SavedDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,FileName,FilePath,SavedAt")] SavedDocument savedDocument)
        {
            if (id != savedDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedDocumentExists(savedDocument.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(savedDocument);
        }

        // GET: SavedDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedDocument = await _context.SavedDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedDocument == null)
            {
                return NotFound();
            }

            return View(savedDocument);
        }

        // POST: SavedDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedDocument = await _context.SavedDocuments.FindAsync(id);
            if (savedDocument != null)
            {
                _context.SavedDocuments.Remove(savedDocument);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedDocumentExists(int id)
        {
            return _context.SavedDocuments.Any(e => e.Id == id);
        }
    }
}
