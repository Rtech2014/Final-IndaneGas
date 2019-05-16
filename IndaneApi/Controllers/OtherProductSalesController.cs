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
    public class OtherProductSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OtherProductSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OtherProductSales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OtherProductSales.Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OtherProductSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherProductSale = await _context.OtherProductSales
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherProductSale == null)
            {
                return NotFound();
            }

            return View(otherProductSale);
        }

        // GET: OtherProductSales/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: OtherProductSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Description,TimeStamp,SaleCount")] OtherProductSale otherProductSale)
        {
            if (ModelState.IsValid)
            {

                _context.Add(otherProductSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", otherProductSale.ProductId);
            return View(otherProductSale);
        }

        // GET: OtherProductSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherProductSale = await _context.OtherProductSales.FindAsync(id);
            if (otherProductSale == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", otherProductSale.ProductId);
            return View(otherProductSale);
        }

        // POST: OtherProductSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Description,TimeStamp,SaleCount")] OtherProductSale otherProductSale)
        {
            if (id != otherProductSale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otherProductSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtherProductSaleExists(otherProductSale.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", otherProductSale.ProductId);
            return View(otherProductSale);
        }

        // GET: OtherProductSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherProductSale = await _context.OtherProductSales
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherProductSale == null)
            {
                return NotFound();
            }

            return View(otherProductSale);
        }

        // POST: OtherProductSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var otherProductSale = await _context.OtherProductSales.FindAsync(id);
            _context.OtherProductSales.Remove(otherProductSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtherProductSaleExists(int id)
        {
            return _context.OtherProductSales.Any(e => e.Id == id);
        }

        //Customized Action
        public async Task<IActionResult> SaleReport()
        {
            var applicationDbContext = await _context.OtherProductSales.Include(l => l.Product).ToListAsync();
            foreach (var item in applicationDbContext)
            {


                item.Income = item.SaleCount * item.Product.SellingPrice;
                item.Profit = item.SaleCount * (item.Product.SellingPrice - item.Product.CostPrice);
            }
            return View(applicationDbContext);
        }
    }
}
