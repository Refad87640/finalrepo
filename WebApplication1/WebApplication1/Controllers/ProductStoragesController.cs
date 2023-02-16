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
    public class ProductStoragesController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public ProductStoragesController(AccoutingSysContext context)
        {
            db = context;
        }

        // GET: api/ProductStorages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStorage>>> GetProductStorages()
        {
            return await db.ProductStorages.ToListAsync();
        }

        // GET: api/ProductStorages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStorage>> GetProductStorage(long id)
        {
            var productStorage = await db.ProductStorages.FindAsync(id);

            if (productStorage == null)
            {
                return NotFound();
            }

            return productStorage;
        }

        // PUT: api/ProductStorages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductStorage(long id, ProductStorage productStorage)
        {
            if (id != productStorage.Id)
            {
                return BadRequest();
            }

            db.Entry(productStorage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStorageExists(id))
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

        // POST: api/ProductStorages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductStorage>> PostProductStorage(ProductStorage productStorage)
        {
            db.ProductStorages.Add(productStorage);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetProductStorage", new { id = productStorage.Id }, productStorage);
        }

        // DELETE: api/ProductStorages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductStorage(long id)
        {
            var productStorage = await db.ProductStorages.FindAsync(id);
            if (productStorage == null)
            {
                return NotFound();
            }

            db.ProductStorages.Remove(productStorage);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductStorageExists(long id)
        {
            return db.ProductStorages.Any(e => e.Id == id);
        }
    }
}
