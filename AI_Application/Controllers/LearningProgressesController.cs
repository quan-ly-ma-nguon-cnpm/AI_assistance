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
    public class LearningProgressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LearningProgressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LearningProgresses
        public async Task<IActionResult> Index()
        {
            return View(await _context.LearningProgresses.ToListAsync());
        }

        // GET: LearningProgresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningProgress = await _context.LearningProgresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learningProgress == null)
            {
                return NotFound();
            }

            return View(learningProgress);
        }

        // GET: LearningProgresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LearningProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Activity,CompletionPercentage,LastUpdated")] LearningProgress learningProgress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learningProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(learningProgress);
        }

        // GET: LearningProgresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningProgress = await _context.LearningProgresses.FindAsync(id);
            if (learningProgress == null)
            {
                return NotFound();
            }
            return View(learningProgress);
        }

        // POST: LearningProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Activity,CompletionPercentage,LastUpdated")] LearningProgress learningProgress)
        {
            if (id != learningProgress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningProgressExists(learningProgress.Id))
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
            return View(learningProgress);
        }

        // GET: LearningProgresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningProgress = await _context.LearningProgresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learningProgress == null)
            {
                return NotFound();
            }

            return View(learningProgress);
        }

        // POST: LearningProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningProgress = await _context.LearningProgresses.FindAsync(id);
            if (learningProgress != null)
            {
                _context.LearningProgresses.Remove(learningProgress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningProgressExists(int id)
        {
            return _context.LearningProgresses.Any(e => e.Id == id);
        }
    }
}
