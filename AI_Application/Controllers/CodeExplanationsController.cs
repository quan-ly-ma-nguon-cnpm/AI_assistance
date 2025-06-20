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
    public class CodeExplanationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CodeExplanationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CodeExplanations
        public async Task<IActionResult> Index()
        {
            return View(await _context.CodeExplanations.ToListAsync());
        }

        // GET: CodeExplanations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeExplanation = await _context.CodeExplanations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (codeExplanation == null)
            {
                return NotFound();
            }

            return View(codeExplanation);
        }

        // GET: CodeExplanations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CodeExplanations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Explanation,RequestedAt")] CodeExplanation codeExplanation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(codeExplanation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(codeExplanation);
        }

        // GET: CodeExplanations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeExplanation = await _context.CodeExplanations.FindAsync(id);
            if (codeExplanation == null)
            {
                return NotFound();
            }
            return View(codeExplanation);
        }

        // POST: CodeExplanations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Explanation,RequestedAt")] CodeExplanation codeExplanation)
        {
            if (id != codeExplanation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(codeExplanation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeExplanationExists(codeExplanation.Id))
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
            return View(codeExplanation);
        }

        // GET: CodeExplanations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeExplanation = await _context.CodeExplanations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (codeExplanation == null)
            {
                return NotFound();
            }

            return View(codeExplanation);
        }

        // POST: CodeExplanations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var codeExplanation = await _context.CodeExplanations.FindAsync(id);
            if (codeExplanation != null)
            {
                _context.CodeExplanations.Remove(codeExplanation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeExplanationExists(int id)
        {
            return _context.CodeExplanations.Any(e => e.Id == id);
        }
    }
}
