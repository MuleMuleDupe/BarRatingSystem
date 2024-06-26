﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarRatingSystem.Data;
using BarRatingSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarRatingSystem.Controllers
{
    public class BarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Bars
        public async Task<IActionResult> Index()
        {
              return _context.Bars != null ? 
                          View(await _context.Bars.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bars'  is null.");
        }

        // GET: Bars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }
        [Authorize(Roles = "Admin")]
        // GET: Bars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl")] Bar bar)
        {
/*            if (ModelState.IsValid)
            {*/
                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
/*            }
            return View(bar);*/
        }
        [Authorize(Roles = "Admin")]

        // GET: Bars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            return View(bar);
        }
        [Authorize(Roles = "Admin")]

        // POST: Bars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl")] Bar bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }
/*
            if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*            }
                        return View(bar);*/
        }
        [Authorize(Roles = "Admin")]

        // GET: Bars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }
        [Authorize(Roles = "Admin")]

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bars'  is null.");
            }
            var bar = await _context.Bars.FindAsync(id);
            if (bar != null)
            {
                _context.Bars.Remove(bar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarExists(int id)
        {
          return (_context.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
