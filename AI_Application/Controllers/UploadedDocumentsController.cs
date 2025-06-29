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
    public class UploadedDocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UploadedDocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UploadedDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.UploadedDocuments.ToListAsync());
        }

        // GET: UploadedDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadedDocument = await _context.UploadedDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uploadedDocument == null)
            {
                return NotFound();
            }

            return View(uploadedDocument);
        }

        // GET: UploadedDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UploadedDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,FileName,FilePath,UploadedAt")] UploadedDocument uploadedDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uploadedDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uploadedDocument);
        }

        // GET: UploadedDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadedDocument = await _context.UploadedDocuments.FindAsync(id);
            if (uploadedDocument == null)
            {
                return NotFound();
            }
            return View(uploadedDocument);
        }

        // POST: UploadedDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,FileName,FilePath,UploadedAt")] UploadedDocument uploadedDocument)
        {
            if (id != uploadedDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uploadedDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UploadedDocumentExists(uploadedDocument.Id))
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
            return View(uploadedDocument);
        }

        // GET: UploadedDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadedDocument = await _context.UploadedDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uploadedDocument == null)
            {
                return NotFound();
            }

            return View(uploadedDocument);
        }

        // POST: UploadedDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uploadedDocument = await _context.UploadedDocuments.FindAsync(id);
            if (uploadedDocument != null)
            {
                _context.UploadedDocuments.Remove(uploadedDocument);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UploadedDocumentExists(int id)
        {
            return _context.UploadedDocuments.Any(e => e.Id == id);
        }
    }
}
