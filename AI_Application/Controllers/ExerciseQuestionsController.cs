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
    public class ExerciseQuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExerciseQuestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExerciseQuestions.ToListAsync());
        }

        // GET: ExerciseQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseQuestion = await _context.ExerciseQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseQuestion == null)
            {
                return NotFound();
            }

            return View(exerciseQuestion);
        }

        // GET: ExerciseQuestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerciseQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer,AskedAt")] ExerciseQuestion exerciseQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exerciseQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exerciseQuestion);
        }

        // GET: ExerciseQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseQuestion = await _context.ExerciseQuestions.FindAsync(id);
            if (exerciseQuestion == null)
            {
                return NotFound();
            }
            return View(exerciseQuestion);
        }

        // POST: ExerciseQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer,AskedAt")] ExerciseQuestion exerciseQuestion)
        {
            if (id != exerciseQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseQuestionExists(exerciseQuestion.Id))
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
            return View(exerciseQuestion);
        }

        // GET: ExerciseQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseQuestion = await _context.ExerciseQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseQuestion == null)
            {
                return NotFound();
            }

            return View(exerciseQuestion);
        }

        // POST: ExerciseQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exerciseQuestion = await _context.ExerciseQuestions.FindAsync(id);
            if (exerciseQuestion != null)
            {
                _context.ExerciseQuestions.Remove(exerciseQuestion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseQuestionExists(int id)
        {
            return _context.ExerciseQuestions.Any(e => e.Id == id);
        }
    }
}
