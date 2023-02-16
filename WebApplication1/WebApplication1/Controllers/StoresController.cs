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
    public class StoresController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public StoresController(AccoutingSysContext context)
        {
            db = context;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> Get(int? id, string? note, int? status, int? limit, long? after)
        {
            if (limit > 50)
                limit = 50;
            else if (limit == 0)
                limit = 10;
            else if (limit < 0)
                limit = (int)Math.Abs((decimal)limit);

            if (id < 1)
                id = null;

            var rows = await db.Stores.Where(row => (id == null
             && (id == null || row.UserId == id)
             && (note == null || row.Name.Contains(note) || (row.Note ?? "").Contains(note))
             && (status == null || row.Status == status)

             && (id > (after ?? 0))
            )
            || row.Id == id
            ).Take(limit ?? 10).ToListAsync();

            if (rows.Count == 0)
                return NoContent();

            return rows;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
            db.Stores.Add(store);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Any(e => e.Id == id);
        }
    }
}
