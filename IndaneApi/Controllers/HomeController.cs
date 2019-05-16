using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IndaneApi.Models;
using IndaneApi.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace IndaneApi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MyEmptyComponent()
        {
            return ViewComponent("EmptyList");
        }

        public IActionResult MyFullComponent()
        {
            return ViewComponent("FullList");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JObject AddFull([FromBody] Full full)
        {
            dynamic fulldata = new Full();
            fulldata.ProductId = full.ProductId;
            fulldata.DeliveryPersonId = full.DeliveryPersonId;
            fulldata.Out_No = full.Out_No;
            fulldata.FullCount = full.FullCount;
            fulldata.CashToBeRecevied = full.CashToBeRecevied;
            fulldata.TimeStamp = DateTime.Now.Date;
            _context.Fulls.Add(fulldata);
            _context.SaveChanges();
            return fulldata;
        }

        public IActionResult GetDelivoryBoyData()
        {
            List<DeliveryPersonDetail> dp = _context.DeliveryPersonDetails.ToList();
            return new JsonResult(dp);
        }

        public IActionResult GetProducts()
        {
            List<Product> dp = _context.Products.ToList();
            return new JsonResult(dp);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CurrentStock()
        {
            double totalFull = 0;
            double totalEmpty = 0;
            double totalcylinder = 0;
            var load = await _context.Loadings.Include(f => f.Product).ToListAsync();

            double LoadFull = 0;
            double LoadEmpty = 0;
           // var Loadcylinder = 0;
            foreach (var item in load)
            {
                var demoFull = item.Full + LoadFull;
                var demoEmpty = item.Empty + LoadEmpty;

                LoadFull = demoFull;
                LoadEmpty = demoEmpty;

                //Loadcylinder = totalFull + totalEmpty;
            }

            var fullDelivery = await _context.Fulls.Include(f => f.DeliveryPersonDetail).Include(f => f.Product).ToListAsync();

            double delFull = 0;
            //var delEmpty = 0;
            //var delcylinder = 0;
            foreach (var full in fullDelivery)
            {
                var demoFull = full.FullCount + delFull;

                delFull = demoFull;
            }
            
            var EmptyDelivery = await _context.Empties.Include(e => e.DeliveryPersonDetail).Include(e => e.Product).ToListAsync();

            double returnFull = 0;
            double returnEmpty = 0;

            foreach (var emp in EmptyDelivery)
            {
                var demoempty = emp.EmptyNo + returnEmpty;
                var demoFull = emp.ReturnedFull + returnFull;

                returnEmpty = demoempty;
                returnFull = demoFull;
            }
            totalFull = LoadFull - delFull + returnFull;
            totalEmpty = LoadEmpty + returnEmpty;
            totalcylinder = totalEmpty + totalFull;

            ViewData["full"] = totalFull;
            ViewData["Empty"] = totalEmpty;
            ViewData["Cylinder"] = totalcylinder;

            return View();
        }

        public async Task<IActionResult> StartStock()
        {
            double totalFull = 0;
            double totalEmpty = 0;
            double totalcylinder = 0;
            var load = await _context.Loadings.Include(f => f.Product).Where(s=> s.TimeStamp.Date == DateTime.Today.AddDays(-1).Date).ToListAsync();

            double LoadFull = 0;
            double LoadEmpty = 0;
            // var Loadcylinder = 0;
            foreach (var item in load)
            {
                var demoFull = item.Full + LoadFull;
                var demoEmpty = item.Empty + LoadEmpty;

                LoadFull = demoFull;
                LoadEmpty = demoEmpty;

                //Loadcylinder = totalFull + totalEmpty;
            }

            var fullDelivery = await _context.Fulls.Include(f => f.DeliveryPersonDetail).Include(f => f.Product).Where(s => s.TimeStamp.Date == DateTime.Today.AddDays(-1).Date).ToListAsync();

            double delFull = 0;
            //var delEmpty = 0;
            //var delcylinder = 0;
            foreach (var full in fullDelivery)
            {
                var demoFull = full.FullCount + delFull;

                delFull = demoFull;
            }



            var EmptyDelivery = await _context.Empties.Include(e => e.DeliveryPersonDetail).Where(s => s.TimeStamp.Date == DateTime.Today.AddDays(-1).Date).Include(e => e.Product).ToListAsync();

            double returnFull = 0;
            double returnEmpty = 0;

            foreach (var emp in EmptyDelivery)
            {
                var demoempty = emp.EmptyNo + returnEmpty;
                var demoFull = emp.ReturnedFull + returnFull;

                returnEmpty = demoempty;
                returnFull = demoFull;
            }
            totalFull = LoadFull - delFull + returnFull;
            totalEmpty = LoadEmpty + returnEmpty;
            totalcylinder = totalEmpty + totalFull;

            ViewData["full"] = totalFull;
            ViewData["Empty"] = totalEmpty;
            ViewData["Cylinder"] = totalcylinder;

            return View();
        }

        public async Task<IActionResult> OtherproductCurrentStock()
        {
            double total = 0;
            var Load = await _context.OtherProductLoads.Include(l => l.Product).ToListAsync();

            double totalLoad = 0; 
            foreach (var item in Load)
            {
                var demo = item.ProductCount + total;

                totalLoad = demo;

            }

            var sale = await _context.OtherProductSales.Include(l => l.Product).ToListAsync();

            double totalSale = 0;
            foreach (var item in sale)
            {
                var demo = item.SaleCount + totalSale;

                totalSale = demo;
            }

            total = totalLoad - totalSale;

            ViewData["TotalLoad"] = totalLoad;
            ViewData["TotalSale"] = totalSale;
            ViewData["Total"] = total;

            return View();
        }

        public async Task<IActionResult> OtherproductstartStock()
        {
            double total = 0;
            var Load = await _context.OtherProductLoads.Include(l => l.Product).Where(s => s.TimeStamp.Date == DateTime.Today.AddDays(-1).Date).ToListAsync();

            double totalLoad = 0;
            foreach (var item in Load)
            {
                var demo = item.ProductCount + total;

                totalLoad = demo;

            }

            var sale = await _context.OtherProductSales.Include(l => l.Product).Where(s => s.TimeStamp.Date == DateTime.Today.AddDays(-1).Date).ToListAsync();

            double totalSale = 0;
            foreach (var item in sale)
            {
                var demo = item.SaleCount + totalSale;

                totalSale = demo;
            }

            total = totalLoad - totalSale;


            ViewData["TotalLoad"] = totalLoad;
            ViewData["TotalSale"] = totalSale;
            ViewData["Total"] = total;

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
