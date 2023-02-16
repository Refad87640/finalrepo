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
    public class ReceiptTypesController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public ReceiptTypesController(AccoutingSysContext context)
        {
            db = context;
        }

        // GET: api/ReceiptTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptType>>> GetReceiptTypes()
        {
            return await db.ReceiptTypes.ToListAsync();
        }

        // GET: api/ReceiptTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptType>> GetReceiptType(int id)
        {
            var receiptType = await db.ReceiptTypes.FindAsync(id);

            if (receiptType == null)
            {
                return NotFound();
            }

            return receiptType;
        }

        // PUT: api/ReceiptTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptType(int id, ReceiptType receiptType)
        {
            if (id != receiptType.Id)
            {
                return BadRequest();
            }

            db.Entry(receiptType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptTypeExists(id))
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

        // POST: api/ReceiptTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceiptType>> PostReceiptType(ReceiptType receiptType)
        {
            db.ReceiptTypes.Add(receiptType);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetReceiptType", new { id = receiptType.Id }, receiptType);
        }

        // DELETE: api/ReceiptTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptType(int id)
        {
            var receiptType = await db.ReceiptTypes.FindAsync(id);
            if (receiptType == null)
            {
                return NotFound();
            }

            db.ReceiptTypes.Remove(receiptType);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptTypeExists(int id)
        {
            return db.ReceiptTypes.Any(e => e.Id == id);
        }
    }
}
