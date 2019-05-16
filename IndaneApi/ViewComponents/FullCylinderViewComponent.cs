using IndaneApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.ViewComponents
{
    [ViewComponent(Name = "FullCylinder")]
    public class FullCylinderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FullCylinderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cylinder19 = _context.Fulls.Include(p => p.Product)
                                                    .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("19"))
                                                    .Sum(s => s.FullCount);
            var cylinder14 = _context.Fulls.Include(p => p.Product)
                                        .Where(s => s.TimeStamp.Date == DateTime.Now.Date && s.Product.Name.Contains("14"))
                                        .Sum(s => s.FullCount);

            ViewData["cyclindre19"] = cylinder19;
            ViewData["cyclindre14"] = cylinder14;
            return View();
        }

    }
}
