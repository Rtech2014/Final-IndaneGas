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
    public class OtherProductLoadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OtherProductLoadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OtherProductLoads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OtherProductLoads.Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OtherProductLoads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherProductLoad = await _context.OtherProductLoads
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherProductLoad == null)
            {
                return NotFound();
            }

            return View(otherProductLoad);
        }

        // GET: OtherProductLoads/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: OtherProductLoads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Description,TimeStamp,LoadNO,ProductCount")] OtherProductLoad otherProductLoad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(otherProductLoad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", otherProductLoad.ProductId);
            return View(otherProductLoad);
        }

        // GET: OtherProductLoads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherProductLoad = await _context.OtherProductLoads.FindAsync(id);
            if (otherProductLoad == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", otherProductLoad.ProductId);
            return View(otherProductLoad);
        }

        // POST: OtherProductLoads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Description,TimeStamp,LoadNO,ProductCount")] OtherProductLoad otherProductLoad)
        {
            if (id != otherProductLoad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otherProductLoad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtherProductLoadExists(otherProductLoad.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", otherProductLoad.ProductId);
            return View(otherProductLoad);
        }

        // GET: OtherProductLoads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherProductLoad = await _context.OtherProductLoads
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherProductLoad == null)
            {
                return NotFound();
            }

            return View(otherProductLoad);
        }

        // POST: OtherProductLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var otherProductLoad = await _context.OtherProductLoads.FindAsync(id);
            _context.OtherProductLoads.Remove(otherProductLoad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtherProductLoadExists(int id)
        {
            return _context.OtherProductLoads.Any(e => e.Id == id);
        }


        //Customized Action
        public async Task<IActionResult> LoadReport()
        {
            var applicationDbContext = await _context.OtherProductLoads.Include(l => l.Product).ToListAsync();
            foreach (var item in applicationDbContext)
            {
                

                item.Expense = item.ProductCount * item.Product.CostPrice;
            }
            return View(applicationDbContext);
        }
    }
}
