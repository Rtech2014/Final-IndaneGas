using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndaneApi.Data;
using IndaneApi.Models;

namespace IndaneApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmptiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmptiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Empties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empty>>> GetEmpties()
        {
            return await _context.Empties.Include(s => s.DeliveryPersonDetail)
                                            .Include(s => s.Product)
                                            .ToListAsync();
        }

        // GET: api/Empties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empty>> GetEmpty(int id)
        {
            var empty = await _context.Empties.FindAsync(id);

            if (empty == null)
            {
                return NotFound();
            }

            return empty;
        }

        // PUT: api/Empties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpty(int id, Empty empty)
        {
            if (id != empty.Id)
            {
                return BadRequest();
            }

            _context.Entry(empty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmptyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Empties
        [HttpPost]
        public async Task<ActionResult<Empty>> PostEmpty(Empty empty)
        {
          //  Stock stock = new Stock();
            var product = empty.ProductId;
            var delivery = empty.DeliveryPersonId;
            var takeCount = empty.In_No;
            //var sellingCost = empty.Product.SellingPrice;
            var datestamp = DateTime.Today.Date;
            var fullDetail = _context.Fulls.Where(s => s.DeliveryPersonId == delivery && s.Out_No == takeCount && s.ProductId == product && s.TimeStamp.Date == datestamp).FirstOrDefault();
            var Prod = _context.Products.Where(s => s.Id == product).FirstOrDefault();
            var del = _context.DeliveryPersonDetails.Where(s => s.Id == delivery).FirstOrDefault();
            var c2R = (empty.EmptyNo + empty.NewConnection) * (Prod.SellingPrice + del.Charges);

            if (fullDetail.FullCount >= empty.EmptyNo && empty.CashRecevied <= c2R)
            {
                empty.TimeStamp = DateTime.Now;
                empty.FullId = fullDetail.Id;

                //stock
                //stock.Name = "Sale";
                //stock.ProductId = empty.ProductId;
                //stock.Full = 0;
                //stock.Empty = empty.EmptyNo;
                //stock.NewConnection = empty.NewConnection;
                //stock.RetrurnedFull = empty.ReturnedFull;
                //stock.TimeStamp = DateTime.Now;


                _context.Add(empty);
//                _context.Add(stock);
                await _context.SaveChangesAsync();
                return StatusCode(200, "Sucessfully Added");
            }
            else
            {
                return StatusCode(500, "Please Enter correct values or entry exists");
            }
        }

        // DELETE: api/Empties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Empty>> DeleteEmpty(int id)
        {
            var empty = await _context.Empties.FindAsync(id);
            if (empty == null)
            {
                return NotFound();
            }

            _context.Empties.Remove(empty);
            await _context.SaveChangesAsync();

            return empty;
        }

        private bool EmptyExists(int id)
        {
            return _context.Empties.Any(e => e.Id == id);
        }
    }
}
