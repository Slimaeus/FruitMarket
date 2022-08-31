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
    public class FruitController : Controller
    {
        private readonly FruitMarketDbContext _context;

        public FruitController(FruitMarketDbContext context)
        {
            _context = context;
        }

        // GET: Fruit
        public async Task<IActionResult> Index()
        {
            var fruitMarketDbContext = _context.Fruits.Include(f => f.Basket);
            return View(await fruitMarketDbContext.ToListAsync());
        }

        // GET: Fruit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var fruit = await _context.Fruits
                .Include(f => f.Basket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruit == null)
            {
                return NotFound();
            }

            return View(fruit);
        }

        // GET: Fruit/Create
        public IActionResult Create()
        {
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id");
            return View();
        }

        // POST: Fruit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Color,Price,BasketId")] Fruit fruit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fruit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id", fruit.BasketId);
            return View(fruit);
        }

        // GET: Fruit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var fruit = await _context.Fruits.FindAsync(id);
            if (fruit == null)
            {
                return NotFound();
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id", fruit.BasketId);
            return View(fruit);
        }

        // POST: Fruit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color,Price,BasketId")] Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fruit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FruitExists(fruit.Id))
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
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id", fruit.BasketId);
            return View(fruit);
        }

        // GET: Fruit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var fruit = await _context.Fruits
                .Include(f => f.Basket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruit == null)
            {
                return NotFound();
            }

            return View(fruit);
        }

        // POST: Fruit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fruits == null)
            {
                return Problem("Entity set 'FruitMarketDbContext.Fruits'  is null.");
            }
            var fruit = await _context.Fruits.FindAsync(id);
            if (fruit != null)
            {
                _context.Fruits.Remove(fruit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FruitExists(int id)
        {
          return (_context.Fruits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
