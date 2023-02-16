using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public InvoicesController(AccoutingSysContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceView>>> GetInvoice(
           long? id, int? invoiceTypeID, long? accountFromID, long? accountToID, int? userID, decimal? minPrice, decimal? maxPrice, int? status, string? note, int? limit, DateTime? beginDate, DateTime? endDate, long? afterID)
        {
            if (limit > 50)
                limit = 50;

            if (limit == 0)
                limit = 10;
            else if (limit < 0)
                limit = (int)Math.Abs((decimal)limit);

            if (minPrice != null && minPrice < 0)
                minPrice = Math.Abs(minPrice ?? 0);

            if (maxPrice != null && maxPrice < 0)
                maxPrice = Math.Abs(maxPrice ?? 0);



            if (id < 1)
                id = null;

            var rows = await db.InvoiceViews.Where(row =>
            (id == null
            && (note == null || row.AccountFromName.ToLower().Contains(note.ToLower()) || row.AccountToName.ToLower().Contains(note.ToLower()) || (row.InvoiceTypeName ?? "").ToLower().Contains(note.ToLower()))
            && (invoiceTypeID == null || row.InvoiceTypeId == invoiceTypeID)
            && (accountFromID == null || row.AccountFromId == accountFromID)
            && (accountToID == null || row.AccountToId == accountToID)
            && (userID == null || row.UserId == userID)
            && (minPrice == null || row.Price >= minPrice)
            && (maxPrice == null || row.Price <= maxPrice)
            && (beginDate == null || row.CreateAt >= beginDate)
            && (endDate == null || row.CreateAt <= endDate)
            && (status == null || row.Status == status)
            && (row.Id > (afterID ?? 0))
            )
            || row.Id == id

            ).Take(limit ?? 10).ToListAsync();

            

            return rows;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(long id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(long id)
        {
            var invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(long id)
        {
            return db.Invoices.Any(e => e.Id == id);
        }
    }
}
