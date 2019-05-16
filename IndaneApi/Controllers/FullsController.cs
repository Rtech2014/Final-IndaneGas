using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndaneApi.Data;
using IndaneApi.Models;

namespace IndaneApi.Controllers
{
    public class FullsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FullsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fulls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext =await _context.Fulls.Include(f => f.DeliveryPersonDetail).Include(f => f.Product).ToListAsync();

            foreach (var item in applicationDbContext)
            {
                item.CashToBeRecevied = item.FullCount * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
            }
            return View(applicationDbContext);
        }

        // GET: Fulls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var full = await _context.Fulls
                .Include(f => f.DeliveryPersonDetail)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (full == null)
            {
                return NotFound();
            }

            return View(full);
        }

        // GET: Fulls/Create
        public IActionResult Create()
        {
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Fulls/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,DeliveryPersonId,FullCount,TimeStamp,Out_No")] Full full)
        {
            if (ModelState.IsValid)
            {
                full.TimeStamp = DateTime.Now;
                _context.Add(full);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", full.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", full.ProductId);
            return View(full);
        }

        // GET: Fulls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var full = await _context.Fulls.FindAsync(id);
            if (full == null)
            {
                return NotFound();
            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", full.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", full.ProductId);
            return View(full);
        }

        // POST: Fulls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,DeliveryPersonId,FullCount,TimeStamp,Out_No")] Full full)
        {
            if (id != full.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(full);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FullExists(full.Id))
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
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", full.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", full.ProductId);
            return View(full);
        }

        // GET: Fulls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var full = await _context.Fulls
                .Include(f => f.DeliveryPersonDetail)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (full == null)
            {
                return NotFound();
            }

            return View(full);
        }

        // POST: Fulls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var full = await _context.Fulls.FindAsync(id);
            _context.Fulls.Remove(full);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FullExists(int id)
        {
            return _context.Fulls.Any(e => e.Id == id);
        }
    }
}
