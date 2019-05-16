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
    public class DeliveryPersonDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPersonDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryPersonDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryPersonDetails.ToListAsync());
        }
        

        // GET: DeliveryPersonDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPersonDetail = await _context.DeliveryPersonDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryPersonDetail == null)
            {
                return NotFound();
            }

            return View(deliveryPersonDetail);
        }

        // GET: DeliveryPersonDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryPersonDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MailId,MobileNo,Charges")] DeliveryPersonDetail deliveryPersonDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryPersonDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryPersonDetail);
        }

        // GET: DeliveryPersonDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPersonDetail = await _context.DeliveryPersonDetails.FindAsync(id);
            if (deliveryPersonDetail == null)
            {
                return NotFound();
            }
            return View(deliveryPersonDetail);
        }

        // POST: DeliveryPersonDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MailId,MobileNo,Charges")] DeliveryPersonDetail deliveryPersonDetail)
        {
            if (id != deliveryPersonDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryPersonDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryPersonDetailExists(deliveryPersonDetail.Id))
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
            return View(deliveryPersonDetail);
        }

        // GET: DeliveryPersonDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPersonDetail = await _context.DeliveryPersonDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryPersonDetail == null)
            {
                return NotFound();
            }

            return View(deliveryPersonDetail);
        }

        // POST: DeliveryPersonDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryPersonDetail = await _context.DeliveryPersonDetails.FindAsync(id);
            _context.DeliveryPersonDetails.Remove(deliveryPersonDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryPersonDetailExists(int id)
        {
            return _context.DeliveryPersonDetails.Any(e => e.Id == id);
        }
    }
}
