using IndaneApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.ViewComponents
{
    [ViewComponent(Name = "FullList")]
    public class FullListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FullListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var items = await _context.Fulls.Include(s => s.DeliveryPersonDetail)
                                            .Include(s => s.Product)
                                            .ToListAsync();
            foreach (var item in items)
            {
                item.CashToBeRecevied = item.FullCount * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
            }

            return View(items);
        } 
    }
}
