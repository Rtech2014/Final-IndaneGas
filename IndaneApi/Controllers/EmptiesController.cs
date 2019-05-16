using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndaneApi.Data;
using IndaneApi.Models;
using System.Globalization;

namespace IndaneApi.Controllers
{
    public class EmptiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmptiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Empties.Include(e => e.DeliveryPersonDetail).Include(e => e.Product);
            foreach (var item in applicationDbContext)
            {
                item.CashToBeRecevied = (item.EmptyNo + item.NewConnection) * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
                item.ReceviedBalance = item.CashToBeRecevied - item.CashRecevied;
            }
            return View(await applicationDbContext.ToListAsync());
        }

        //14.2 cylinder report
        public async Task<IActionResult> Cylinder1()
        {
            var cylinder = await _context.Empties.Include(e => e.DeliveryPersonDetail).Include(s => s.Product).Where(a => a.Product.Name.Contains("14")).ToListAsync();
            foreach (var item in cylinder)
            {
                //if (item.CashToBeRecevied != 0)
                //{
                    item.CashToBeRecevied = (item.EmptyNo + item.NewConnection) * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
                    item.ReceviedBalance = item.CashToBeRecevied - item.CashRecevied;
                //}
            }
            return View(cylinder);
        }

        //19Kg cylinder report
        public async Task<IActionResult> Cylinder2()
        {
            //if (joinDate != null && fromDate != null)
            //{
            //    //this will default to current date if for whatever reason the date supplied by user did not parse successfully

            //    DateTime start = DateManager.GetDate(joinDate) ?? DateTime.Now;

            //    DateTime end = DateManager.GetDate(fromDate) ?? DateTime.Now;

            //    var rangeData = await _context.Empties
            //                                  .Include(e => e.DeliveryPersonDetail)
            //                                  .Include(s => s.Product)
            //                                  .Where(x => x.TimeStamp.DayOfWeek >= start.DayOfWeek && x.TimeStamp.DayOfWeek <= end.DayOfWeek && x.Product.Name.Contains("19")).ToListAsync();
            //    foreach (var item in rangeData)
            //    {
            //        if (item != null)
            //        {
            //            if (item.CashToBeRecevied != 0)
            //            {
            //                item.CashToBeRecevied = (item.EmptyNo + item.NewConnection) * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
            //                item.ReceviedBalance = item.CashToBeRecevied - item.CashRecevied;
            //            }
            //        }
            //    }

            //    return View(rangeData);
            //}

            var cylinder = await _context.Empties.Include(e => e.DeliveryPersonDetail).Include(s => s.Product).Where(a => a.Product.Name.Contains("19")).ToListAsync();
            foreach (var item in cylinder)
            {
                if (item != null)
                {
                    item.CashToBeRecevied = (item.EmptyNo + item.NewConnection) * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
                    item.ReceviedBalance = item.CashToBeRecevied - item.CashRecevied;
                    item.TimeStamp.Date.ToString("MM/dd/yyyy");
                }
            }
            return View(cylinder);
        }

        // GET: Empties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empty = await _context.Empties
                .Include(e => e.DeliveryPersonDetail)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empty == null)
            {
                return NotFound();
            }

            return View(empty);
        }

        // GET: Empties/Create
        public IActionResult Create()
        {
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Empties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,DeliveryPersonId,TimeStamp,In_No,EmptyNo,NewConnection,EmptyPending,ReturnedFull,CashRecevied")] Empty empty)
        {

            if (ModelState.IsValid)
            {
                var product = empty.ProductId;
                var delivery = empty.DeliveryPersonId;
                var takeCount = empty.In_No;
                var datestamp = DateTime.Today.Date;
                var fullDetail = _context.Fulls.Where(s => s.DeliveryPersonId == delivery && s.Out_No == takeCount && s.ProductId == product && s.TimeStamp.Date == datestamp).FirstOrDefault();

                if (fullDetail.FullCount >= empty.EmptyNo)
                {
                    empty.TimeStamp = DateTime.Now;
                    empty.FullId = fullDetail.Id;
                    _context.Add(empty);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    RedirectToAction("ErrorInfo", "Empties");
                    //ViewData["ErrorInfo"] = "Refill Cylinder count exceeds the limit.please enter correct value.";
                }

            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", empty.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", empty.ProductId);
            return View(empty);
        }

        // GET: Empties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empty = await _context.Empties.FindAsync(id);
            if (empty == null)
            {
                return NotFound();
            }
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", empty.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", empty.ProductId);
            return View(empty);
        }

        // POST: Empties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,DeliveryPersonId,TimeStamp,In_No,EmptyNo,NewConnection,EmptyPending,ReturnedFull,CashRecevied")] Empty empty)
        {
            if (id != empty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmptyExists(empty.Id))
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
            ViewData["DeliveryPersonId"] = new SelectList(_context.DeliveryPersonDetails, "Id", "Id", empty.DeliveryPersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", empty.ProductId);
            return View(empty);
        }

        // GET: Empties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empty = await _context.Empties
                .Include(e => e.DeliveryPersonDetail)
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empty == null)
            {
                return NotFound();
            }

            return View(empty);
        }

        // POST: Empties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empty = await _context.Empties.FindAsync(id);
            _context.Empties.Remove(empty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmptyExists(int id)
        {
            return _context.Empties.Any(e => e.Id == id);
        }



    }

    public class DateManager
    {
        /// <summary>
        /// Use to prevent month from being overritten when day is less than or equal 12
        /// </summary>
        static bool IsMonthAssigned { get; set; }



        public static DateTime? GetDate(string d)
        {
            char[] splitsoptions = { '/', '-', ' ' };
            foreach (var i in splitsoptions)
            {
                var y = 0;
                var m = 0;
                var day = 0;
                if (d.IndexOf(i) > 0)
                {
                    try
                    {
                        foreach (var e in d.Split(i))
                        {


                            if (e.Length == 4)
                            {
                                y = Convert.ToInt32(e);

                                continue;
                            }
                            if (Convert.ToInt32(e) <= 12 && !IsMonthAssigned)
                            {
                                m = Convert.ToInt32(e);
                                IsMonthAssigned = true;
                                continue;
                            }
                            day = Convert.ToInt32(e);


                        }

                        return new DateTime(y, m, day);
                    }
                    catch
                    {
                        //We are silent about this but we  could set a message about wrong date input in ViewBag    and display to user if this  this method returns null
                    }
                }
            }
            return null;


        }
        // Another overload. this will catch more date formats without manually checking as above

        public static DateTime? GetDate(string d, bool custom)
        {
            CultureInfo culture = new CultureInfo("en-US");

            string[] dateFormats =
            {
                "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "yyyy/dd/MM", "dd-MM-yyyy", "MM-dd-yyyy", "yyyy-MM-dd",
                "yyyy-dd-MM", "dd MM yyyy", "MM dd yyyy", "yyyy MM dd", "yyyy dd MM", "dd.MM.yyyy", "MM.dd.yyyy",
                "yyyy.MM.dd", "yyyy.dd.MM","yyyyMMdd","yyyyddMM","MMddyyyy","ddMMyyyy"
            };//add your own to the array if any

            culture.DateTimeFormat.SetAllDateTimePatterns(dateFormats, 'Y');

            if (DateTime.TryParseExact(d, dateFormats, culture, DateTimeStyles.None, out var date))
                return date;

            return null;


        }

    }
}