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
    public class ReceiptsController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public ReceiptsController(AccoutingSysContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptView>>> get(
          long? id, int? receiptTypeID, long? accountFromID, long? accountToID, int? userID, decimal? minPrice, decimal? maxPrice, int? status, string? note, DateTime? beginDate, DateTime? endDate, long? afterID, int? limit)
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

            var rows = await db.ReceiptViews.Where(row =>
            (id == null
            && (note == null || (row.Note ?? "").Contains(note) || row.AccountFromName.Contains(note) || row.AccountToName.Contains(note) || row.ReceiptTypeName.Contains(note))
            && (receiptTypeID == null || row.ReceiptTypeId == receiptTypeID)
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

            if (rows.Count == 0)
            {
                return NotFound();
            }

            return rows;
        }


        // PUT: api/Receipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(long id, Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return BadRequest();
            }

            db.Entry(receipt).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/Receipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receipt>> PostReceipt(Receipt receipt)
        {
            db.Receipts.Add(receipt);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetReceipt", new { id = receipt.Id }, receipt);
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(long id)
        {
            var receipt = await db.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            db.Receipts.Remove(receipt);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptExists(long id)
        {
            return db.Receipts.Any(e => e.Id == id);
        }
    }
}
