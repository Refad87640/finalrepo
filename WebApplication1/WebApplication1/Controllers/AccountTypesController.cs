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
    public class AccountTypesController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public AccountTypesController(AccoutingSysContext context)
        {
            db = context;
        }

        //GET: api/AccountTypes
       [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountTypes()
        {
            return await db.AccountTypes.ToListAsync();
        }

        //GET: api/AccountTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountType>> GetAccountType(int id)
        {
            var accountType = await db.AccountTypes.FindAsync(id);

            if (accountType == null)
            {
                return NotFound();
            }

            return accountType;
        }

        //PUT: api/AccountTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountType(int id, AccountType accountType)
        {
            if (id != accountType.Id)
            {
                return BadRequest();
            }

            db.Entry(accountType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTypeExists(id))
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

        //POST: api/AccountTypes
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountType>> PostAccountType(AccountType accountType)
        {
            db.AccountTypes.Add(accountType);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetAccountType", new { id = accountType.Id }, accountType);
        }

        //DELETE: api/AccountTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountType(int id)
        {
            var accountType = await db.AccountTypes.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }

            db.AccountTypes.Remove(accountType);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountTypeExists(int id)
        {
            return db.AccountTypes.Any(e => e.Id == id);
        }
    }
}
