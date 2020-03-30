using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentMusic.Data;
using AssignmentMusic.Models;
using Microsoft.AspNetCore.Authorization;

namespace AssignmentMusic.Controllers
{
    [Authorize]
    public class MusicProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MusicProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: MusicProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MusicProducts.Include(p => p.Company).OrderBy(p => p.CompanyID);
            return View("Index", await _context.MusicProducts.ToListAsync());
        }
        [AllowAnonymous]
        // GET: MusicProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicProducts = await _context.MusicProducts
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (musicProducts == null)
            {
                return NotFound();
            }

            return View(musicProducts);
        }

        // GET: MusicProducts/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "CompanyName");
            return View();
        }

        // POST: MusicProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyID,ProductId,ProductName,Model,Price,ReleasedDate")] MusicProducts musicProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "CompanyName");
            return View(musicProducts);
        }

        // GET: MusicProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicProducts = await _context.MusicProducts.FindAsync(id);
            if (musicProducts == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "CompanyName");
            return View(musicProducts);
        }

        // POST: MusicProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyID,ProductId,ProductName,Model,Price,ReleasedDate")] MusicProducts musicProducts)
        {
            if (id != musicProducts.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicProductsExists(musicProducts.ProductId))
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
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "CompanyName");
            return View(musicProducts);
        }

        // GET: MusicProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicProducts = await _context.MusicProducts
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (musicProducts == null)
            {
                return NotFound();
            }

            return View(musicProducts);
        }

        // POST: MusicProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicProducts = await _context.MusicProducts.FindAsync(id);
            _context.MusicProducts.Remove(musicProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicProductsExists(int id)
        {
            return _context.MusicProducts.Any(e => e.ProductId == id);
        }
    }
}
