using IndaneApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.ViewComponents
{
    [ViewComponent(Name = "EmptyCylinder")]
    public class EmptyCylinderViewComponent :ViewComponent 
    {
        private readonly ApplicationDbContext _context;
        public EmptyCylinderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cylinder19 = _context.Empties.Include(p => p.Product)
                                                    .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                                                    .Sum(s => s.EmptyNo);
            var cylinder14 = _context.Empties.Include(p => p.Product)
                                        .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                                        .Sum(s => s.EmptyNo);
            var cylinderNC19 = _context.Empties.Include(p => p.Product)
                            .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                            .Sum(s => s.NewConnection);
            var cylinderNC14 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                .Sum(s => s.NewConnection);
            var cylinderEp14 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                .Sum(s => s.EmptyPending);
            var cylinderEp19 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                .Sum(s => s.EmptyPending);
            var cylinderRf14 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                .Sum(s => s.ReturnedFull);
            var cylinderRf19 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                .Sum(s => s.ReturnedFull);
            var cylinderC2r14 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                .Sum(s => s.CashToBeRecevied);
            var cylinderC2r19 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                .Sum(s => s.CashToBeRecevied);
            var cylinderCr14 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                .Sum(s => s.CashRecevied);
            var cylinderCr19 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                .Sum(s => s.CashRecevied);
            var cylinderRb14 = _context.Empties.Include(p => p.Product)
                    .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                    .Sum(s => s.ReceviedBalance);
            var cylinderRb19 = _context.Empties.Include(p => p.Product)
                .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                .Sum(s => s.ReceviedBalance);



            ViewData["cyclinder19"] = cylinder19;
            ViewData["cyclinder14"] = cylinder14;

            ViewData["cyclinderNC19"] = cylinderNC19;
            ViewData["cyclinderNC14"] = cylinderNC14;

            ViewData["cyclinderEp19"] = cylinderEp19;
            ViewData["cyclinderEp14"] = cylinderEp14;

            ViewData["cyclinderRf19"] = cylinderRf19;
            ViewData["cyclinderRf14"] = cylinderRf14;

            ViewData["cyclinderC2r19"] = cylinderC2r19;
            ViewData["cyclinderC2r14"] = cylinderC2r14;

            ViewData["cyclinderCr19"] = cylinderCr19;
            ViewData["cyclinderCr14"] = cylinderCr14;

            ViewData["cyclinderRb19"] = cylinderRb19;
            ViewData["cyclinderRb14"] = cylinderRb14;

            return View();
        }
    }
}
