using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AI_Application.Data;
using AI_Application.Models;

namespace AI_Application.Controllers
{
    public class KnowledgeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KnowledgeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Knowledge
        public async Task<IActionResult> Index()
        {
            return View(await _context.KnowledgeCategories.ToListAsync());
        }

        // GET: Knowledge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledgeCategory = await _context.KnowledgeCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledgeCategory == null)
            {
                return NotFound();
            }

            return View(knowledgeCategory);
        }

        // GET: Knowledge/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Knowledge/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] KnowledgeCategory knowledgeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowledgeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowledgeCategory);
        }

        // GET: Knowledge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledgeCategory = await _context.KnowledgeCategories.FindAsync(id);
            if (knowledgeCategory == null)
            {
                return NotFound();
            }
            return View(knowledgeCategory);
        }

        // POST: Knowledge/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] KnowledgeCategory knowledgeCategory)
        {
            if (id != knowledgeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowledgeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeCategoryExists(knowledgeCategory.Id))
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
            return View(knowledgeCategory);
        }

        // GET: Knowledge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowledgeCategory = await _context.KnowledgeCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledgeCategory == null)
            {
                return NotFound();
            }

            return View(knowledgeCategory);
        }

        // POST: Knowledge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowledgeCategory = await _context.KnowledgeCategories.FindAsync(id);
            if (knowledgeCategory != null)
            {
                _context.KnowledgeCategories.Remove(knowledgeCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowledgeCategoryExists(int id)
        {
            return _context.KnowledgeCategories.Any(e => e.Id == id);
        }
    }
}
