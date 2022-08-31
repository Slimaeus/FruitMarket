using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FruitMarket.Data;
using FruitMarket.Models;

namespace FruitMarket.Controllers
{
    public class BasketController : Controller
    {
        private readonly FruitMarketDbContext _context;

        public BasketController(FruitMarketDbContext context)
        {
            _context = context;
        }

        // GET: Basket
        public async Task<IActionResult> Index()
        {
            var fruitMarketDbContext = _context.Baskets.Include(b => b.Customer);
            return View(await fruitMarketDbContext.ToListAsync());
        }

        // GET: Basket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Baskets == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets
                .Include(b => b.Customer).Include(b => b.Fruits)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // GET: Basket/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Basket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,CustomerId")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", basket.CustomerId);
            return View(basket);
        }

        // GET: Basket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Baskets == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", basket.CustomerId);
            return View(basket);
        }

        // POST: Basket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,CustomerId")] Basket basket)
        {
            if (id != basket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketExists(basket.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", basket.CustomerId);
            return View(basket);
        }

        // GET: Basket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Baskets == null)
            {
                return NotFound();
            }

            var basket = await _context.Baskets
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // POST: Basket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Baskets == null)
            {
                return Problem("Entity set 'FruitMarketDbContext.Baskets'  is null.");
            }
            var basket = await _context.Baskets.FindAsync(id);
            if (basket != null)
            {
                _context.Baskets.Remove(basket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketExists(int id)
        {
          return (_context.Baskets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
