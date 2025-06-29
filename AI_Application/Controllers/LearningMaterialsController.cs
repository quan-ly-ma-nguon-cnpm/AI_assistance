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
    public class LearningMaterialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LearningMaterialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LearningMaterials
        public async Task<IActionResult> Index()
        {
            return View(await _context.LearningMaterials.ToListAsync());
        }

        // GET: LearningMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningMaterial = await _context.LearningMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learningMaterial == null)
            {
                return NotFound();
            }

            return View(learningMaterial);
        }

        // GET: LearningMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LearningMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,FilePath,UploadedBy")] LearningMaterial learningMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learningMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(learningMaterial);
        }

        // GET: LearningMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningMaterial = await _context.LearningMaterials.FindAsync(id);
            if (learningMaterial == null)
            {
                return NotFound();
            }
            return View(learningMaterial);
        }

        // POST: LearningMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,FilePath,UploadedBy")] LearningMaterial learningMaterial)
        {
            if (id != learningMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningMaterialExists(learningMaterial.Id))
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
            return View(learningMaterial);
        }

        // GET: LearningMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningMaterial = await _context.LearningMaterials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (learningMaterial == null)
            {
                return NotFound();
            }

            return View(learningMaterial);
        }

        // POST: LearningMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningMaterial = await _context.LearningMaterials.FindAsync(id);
            if (learningMaterial != null)
            {
                _context.LearningMaterials.Remove(learningMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningMaterialExists(int id)
        {
            return _context.LearningMaterials.Any(e => e.Id == id);
        }
    }
}
