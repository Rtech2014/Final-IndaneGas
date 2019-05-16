using IndaneApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.ViewComponents
{
    [ViewComponent(Name = "EmptyList")]
    public class EmptyListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public EmptyListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var applicationDbContext = _context.Empties.Include(e => e.DeliveryPersonDetail).Include(e => e.Product);
            foreach (var item in applicationDbContext)
            {
                item.CashToBeRecevied = (item.EmptyNo + item.NewConnection) * (item.Product.SellingPrice + item.DeliveryPersonDetail.Charges);
                item.ReceviedBalance = item.CashToBeRecevied - item.CashRecevied;
            }
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
