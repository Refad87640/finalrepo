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
    public class ProductsController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public ProductsController(AccoutingSysContext context)
        {
            db = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductView>>> Get(long? id, string? note, long? productTypeID, decimal? minPrice, decimal? maxPrice, int? status, int? userID, int? limit, long? afterID)
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

            var rows = await db.ProductViews.Where(row => (id == null
            && (note == null || (row.Note ?? "").Contains(note) || row.Name.Contains(note) || row.ProductTypeName.Contains(note))
            && (productTypeID == null || row.ProductTypeId == productTypeID)
            && (minPrice == null || row.Price >= minPrice)
            && (maxPrice == null || row.Price <= maxPrice)
            && (status == null || row.Status == status)
            && (userID == null || row.UserId == userID)
            && (row.Id > (afterID ?? 0))
            )
            || row.Id == id

            ).Take(limit ?? 10).ToListAsync();

            if (rows.Count == 0)
                return NoContent();
            return rows;
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(long id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return db.Products.Any(e => e.Id == id);
        }
    }
}
