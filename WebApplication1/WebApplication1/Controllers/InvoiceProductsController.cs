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
    public class InvoiceProductsController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public InvoiceProductsController(AccoutingSysContext context)
        {
            db = context;
        }

        //GET: api/InvoiceProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceProductView>>> GetInvoiceProducts(long? id, long? invoiceID, long? productID, int? storeID, int? productTypeID, int? maxCount, int? minCount, decimal? minPrice, decimal? maxPrice, string? note, long? afterID, int? limit)
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
            
            if (minCount != null && minCount < 0)
                minCount = Math.Abs(minCount ?? 0);

            if (maxCount != null && maxCount < 0)
                maxCount = Math.Abs(maxCount ?? 0);



            if (id < 1)
                id = null;

            var rows = await db.InvoiceProductViews.Where(row =>
            (id == null
            && (invoiceID == null || row.InvoiceId == invoiceID)
            && (productID == null || row.ProductId == productID)
            && (productTypeID == null || row.ProductTypeId == productTypeID)
            && (storeID == null || row.StoreId== storeID)

            && (minPrice == null || row.Price >= minPrice)
            && (maxPrice == null || row.Price <= maxPrice)
            && (minCount == null || row.Count >= minCount)
            && (maxCount == null || row.Count <= maxCount)
            && (note == null || (row.Note??"").Contains(note) || row.StoreName.Contains(note) || row.ProductName.Contains(note) || row.ProductTypeName.Contains(note))

            && (row.Id > (afterID ?? 0))
            )
            || row.Id == id).Take(limit ?? 10).ToListAsync();

            if (rows.Count == 0)
            {
                return NotFound();
            }
            return rows;
        }


        //PUT: api/InvoiceProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceProduct(long id, InvoiceProduct invoiceProduct)
        {
            if (id != invoiceProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(invoiceProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceProductExists(id))
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

        //POST: api/InvoiceProducts
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceProduct>> PostInvoiceProduct(InvoiceProduct invoiceProduct)
        {
            db.InvoiceProducts.Add(invoiceProduct);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceProduct", new { id = invoiceProduct.Id }, invoiceProduct);
        }

        //DELETE: api/InvoiceProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceProduct(long id)
        {
            var invoiceProduct = await db.InvoiceProducts.FindAsync(id);
            if (invoiceProduct == null)
            {
                return NotFound();
            }

            db.InvoiceProducts.Remove(invoiceProduct);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceProductExists(long id)
        {
            return db.InvoiceProducts.Any(e => e.Id == id);
        }
    }
}
