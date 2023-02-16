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
    public class PermissionUsersController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public PermissionUsersController(AccoutingSysContext context)
        {
            db = context;
        }

        // GET: api/PermissionUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionUser>>> GetPermissionUsers()
        {
            return await db.PermissionUsers.ToListAsync();
        }

        // GET: api/PermissionUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionUser>> GetPermissionUser(long id)
        {
            var permissionUser = await db.PermissionUsers.FindAsync(id);

            if (permissionUser == null)
            {
                return NotFound();
            }

            return permissionUser;
        }

        // PUT: api/PermissionUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermissionUser(long id, PermissionUser permissionUser)
        {
            if (id != permissionUser.Id)
            {
                return BadRequest();
            }

            db.Entry(permissionUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionUserExists(id))
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

        // POST: api/PermissionUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PermissionUser>> PostPermissionUser(PermissionUser permissionUser)
        {
            db.PermissionUsers.Add(permissionUser);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetPermissionUser", new { id = permissionUser.Id }, permissionUser);
        }

        // DELETE: api/PermissionUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissionUser(long id)
        {
            var permissionUser = await db.PermissionUsers.FindAsync(id);
            if (permissionUser == null)
            {
                return NotFound();
            }

            db.PermissionUsers.Remove(permissionUser);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionUserExists(long id)
        {
            return db.PermissionUsers.Any(e => e.Id == id);
        }
    }
}
