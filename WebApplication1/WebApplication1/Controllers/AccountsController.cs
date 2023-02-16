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
    public class AccountsController : ControllerBase
    {
        private readonly AccoutingSysContext db;

        public AccountsController(AccoutingSysContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountView>>> get(
            long? id, string? note, int? userID, int? accountTypeID, int? status, DateTime? beginDate, DateTime? endDate, decimal? minBalance, decimal? maxBalance, int? limit, long? afterID)
        {

            if (limit > 50)
                limit = 50;

            if (limit == 0)
                limit = 10;
            else if (limit < 0)
                limit = (int)Math.Abs((decimal)limit);
            if (id < 1)
                id = null;
            var rows = await db.AccountViews.Where(row =>
            (id == null
            && (userID == null || row.UserId == userID) &&
            (accountTypeID == null || row.AccountTypeId == accountTypeID) &&
            (status == null || row.Status == status) &&
            (beginDate == null || row.CreateAt >= beginDate) &&
             (endDate == null || row.CreateAt <= endDate) &&
             (minBalance == null || row.Balance >= minBalance) &&
             (maxBalance == null || row.Balance <= maxBalance) &&
             (note == null || (row.Note ?? "").Contains(note)
             || row.Name.Contains(note) || row.AccountTypeName.Contains(note)) &&
             (row.Id > (afterID ?? 0))
             )
       || row.Id == id
       ).Take(limit ?? 10).ToListAsync();

            if (rows.Count == 0)
                return NotFound();

            return rows;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountView>> GetAccount(long id)
        {
            var account = await db.AccountViews.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        //PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(long id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            db.Entry(account).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        //POST: api/Accounts
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            db.Accounts.Add(account);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        //DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(long id)
        {
            var account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(long id)
        {
            return db.Accounts.Any(e => e.Id == id);
        }
    }
}
