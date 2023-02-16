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
    public class InvoiceTypesController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public InvoiceTypesController(AccoutingSysContext context)
        {
            db = context;
        }

        // GET: api/InvoiceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceType>>> GetInvoiceTypes()
        {
            return await db.InvoiceTypes.ToListAsync();
        }

        // GET: api/InvoiceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceType>> GetInvoiceType(int id)
        {
            var invoiceType = await db.InvoiceTypes.FindAsync(id);

            if (invoiceType == null)
            {
                return NotFound();
            }

            return invoiceType;
        }

        // PUT: api/InvoiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceType(int id, InvoiceType invoiceType)
        {
            if (id != invoiceType.Id)
            {
                return BadRequest();
            }

            db.Entry(invoiceType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceTypeExists(id))
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

        // POST: api/InvoiceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceType>> PostInvoiceType(InvoiceType invoiceType)
        {
            db.InvoiceTypes.Add(invoiceType);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceType", new { id = invoiceType.Id }, invoiceType);
        }

        // DELETE: api/InvoiceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceType(int id)
        {
            var invoiceType = await db.InvoiceTypes.FindAsync(id);
            if (invoiceType == null)
            {
                return NotFound();
            }

            db.InvoiceTypes.Remove(invoiceType);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceTypeExists(int id)
        {
            return db.InvoiceTypes.Any(e => e.Id == id);
        }
    }
}
