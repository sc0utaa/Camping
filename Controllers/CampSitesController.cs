using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Camping.Data;
using Camping.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Camping.Controllers
{
    public class CampSitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CampSitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CampSites
        public async Task<IActionResult> Index()
        {
              return _context.CampSite != null ? 
                          View(await _context.CampSite.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CampSite'  is null.");
        }
        [Authorize]
        // GET: CampSites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CampSite == null)
            {
                return NotFound();
            }

            var campSite = await _context.CampSite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campSite == null)
            {
                return NotFound();
            }

            return View(campSite);
        }
        [Authorize(Roles = "Admin")]
        // GET: CampSites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CampSites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Latitude,Longitude,ImagePath")] CampSite campSite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campSite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campSite);
        }
        [Authorize(Roles = "Admin")]
        // GET: CampSites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CampSite == null)
            {
                return NotFound();
            }

            var campSite = await _context.CampSite.FindAsync(id);
            if (campSite == null)
            {
                return NotFound();
            }
            return View(campSite);
        }
        [Authorize(Roles = "Admin")]
        // POST: CampSites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Latitude,Longitude,ImagePath")] CampSite campSite)
        {
            if (id != campSite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campSite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampSiteExists(campSite.Id))
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
            return View(campSite);
        }
        [Authorize(Roles = "Admin")]
        // GET: CampSites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CampSite == null)
            {
                return NotFound();
            }

            var campSite = await _context.CampSite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campSite == null)
            {
                return NotFound();
            }

            return View(campSite);
        }
        [Authorize(Roles = "Admin")]
        // POST: CampSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CampSite == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CampSite'  is null.");
            }
            var campSite = await _context.CampSite.FindAsync(id);
            if (campSite != null)
            {
                _context.CampSite.Remove(campSite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampSiteExists(int id)
        {
          return (_context.CampSite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
