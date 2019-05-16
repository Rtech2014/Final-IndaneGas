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
    public class LoadingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoadingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loadings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Loadings.Include(l => l.Product).ToListAsync();
            foreach (var item in applicationDbContext)
            {
                if (item.LoadType == "2way")
                {
                    
                    item.Empty = item.Full;
                   
                }
                else
                {
                   
                    item.Empty = 0;
                    
                }
            }
            return View( applicationDbContext);
        }

        // GET: Loadings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loading = await _context.Loadings
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loading == null)
            {
                return NotFound();
            }

            return View(loading);
        }

        // GET: Loadings/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Loadings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,LoadType,TimeStamp,LoadCount,Full")] Loading loading)
        {
            if (ModelState.IsValid)
            {
                loading.TimeStamp = DateTime.Now;
                _context.Add(loading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", loading.ProductId);
            return View(loading);
        }

        // GET: Loadings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loading = await _context.Loadings.FindAsync(id);
            if (loading == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", loading.ProductId);
            return View(loading);
        }

        // POST: Loadings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,LoadType,TimeStamp,LoadCount,Full")] Loading loading)
        {
            if (id != loading.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoadingExists(loading.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", loading.ProductId);
            return View(loading);
        }

        // GET: Loadings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loading = await _context.Loadings
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loading == null)
            {
                return NotFound();
            }

            return View(loading);
        }

        // POST: Loadings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loading = await _context.Loadings.FindAsync(id);
            _context.Loadings.Remove(loading);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoadingExists(int id)
        {
            return _context.Loadings.Any(e => e.Id == id);
        }


        //Customized Action
        public async Task<IActionResult> LoadReport()
        {
            var applicationDbContext = await _context.Loadings.Include(l => l.Product).ToListAsync();
            foreach (var item in applicationDbContext)
            {
                if (item.LoadType == "2way")
                {

                    item.Empty = item.Full;

                }
                else
                {

                    item.Empty = 0;

                }

                item.Expense = item.Full * item.Product.CostPrice;
            }
            return View(applicationDbContext);
        }
    }
}
