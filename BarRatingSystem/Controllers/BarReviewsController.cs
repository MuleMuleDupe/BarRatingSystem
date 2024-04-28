using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarRatingSystem.Data;
using BarRatingSystem.Models;

namespace BarRatingSystem.Controllers
{
    public class BarReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BarReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BarReviews.Include(b => b.Bar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BarReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BarReviews == null)
            {
                return NotFound();
            }

            var barReview = await _context.BarReviews
                .Include(b => b.Bar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barReview == null)
            {
                return NotFound();
            }

            return View(barReview);
        }

        // GET: BarReviews/Create
        public IActionResult Create()
        {
            ViewData["BarId"] = new SelectList(_context.Bars, "Id", "Description");
            return View();
        }

        // POST: BarReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BarId,Review,Rating")] BarReview barReview)
        {
/*            if (ModelState.IsValid)
            {*/
                barReview.UserId = User.Identity.Name;
                _context.Add(barReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
/*            }*/
            ViewData["BarId"] = new SelectList(_context.Bars, "Id", "Description", barReview.BarId);
/*            return View(barReview);*/
        }


        // GET: BarReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BarReviews == null)
            {
                return NotFound();
            }

            var barReview = await _context.BarReviews.FindAsync(id);
            if (barReview == null)
            {
                return NotFound();
            }
            ViewData["BarId"] = new SelectList(_context.Bars, "Id", "Description", barReview.BarId);
            return View(barReview);
        }

        // POST: BarReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BarId,UserId,Review,Rating")] BarReview barReview)
        {
            if (id != barReview.Id)
            {
                return NotFound();
            }

/*            if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(barReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarReviewExists(barReview.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
/*            }*/
            ViewData["BarId"] = new SelectList(_context.Bars, "Id", "Description", barReview.BarId);
/*            return View(barReview);*/
        }

        // GET: BarReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BarReviews == null)
            {
                return NotFound();
            }

            var barReview = await _context.BarReviews
                .Include(b => b.Bar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barReview == null)
            {
                return NotFound();
            }

            return View(barReview);
        }

        // POST: BarReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BarReviews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BarReviews'  is null.");
            }
            var barReview = await _context.BarReviews.FindAsync(id);
            if (barReview != null)
            {
                _context.BarReviews.Remove(barReview);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarReviewExists(int id)
        {
          return (_context.BarReviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
