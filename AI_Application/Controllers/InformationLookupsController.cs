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
    public class InformationLookupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InformationLookupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InformationLookups
        public async Task<IActionResult> Index()
        {
            return View(await _context.InformationLookups.ToListAsync());
        }

        // GET: InformationLookups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationLookup = await _context.InformationLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationLookup == null)
            {
                return NotFound();
            }

            return View(informationLookup);
        }

        // GET: InformationLookups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InformationLookups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Query,Result,StudentId,SearchedAt")] InformationLookup informationLookup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationLookup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informationLookup);
        }

        // GET: InformationLookups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationLookup = await _context.InformationLookups.FindAsync(id);
            if (informationLookup == null)
            {
                return NotFound();
            }
            return View(informationLookup);
        }

        // POST: InformationLookups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Query,Result,StudentId,SearchedAt")] InformationLookup informationLookup)
        {
            if (id != informationLookup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informationLookup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationLookupExists(informationLookup.Id))
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
            return View(informationLookup);
        }

        // GET: InformationLookups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationLookup = await _context.InformationLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationLookup == null)
            {
                return NotFound();
            }

            return View(informationLookup);
        }

        // POST: InformationLookups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informationLookup = await _context.InformationLookups.FindAsync(id);
            if (informationLookup != null)
            {
                _context.InformationLookups.Remove(informationLookup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationLookupExists(int id)
        {
            return _context.InformationLookups.Any(e => e.Id == id);
        }
    }
}
