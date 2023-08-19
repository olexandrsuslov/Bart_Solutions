using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBartSolutions.DbContext;
using TestBartSolutions.Models;

namespace TestBartSolutions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountsContext _context;

        public AccountController(AccountsContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
          if (_context.Accounts == null)
          {
              return Problem("Entity set 'AccountsContext.Accounts'  is null.");
          }
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }
        
        [HttpPost("Dto")]
        public async Task<ActionResult<Account>> PostDto(RequestDto requestDto)
        {
            var accounts = await _context.Accounts.ToListAsync();
            Console.WriteLine(accounts);
            if (!accounts.Any(a => a.Name == requestDto.AccountName))
            {
                return NotFound();
            }

            var account = accounts.First(a => a.Name == requestDto.AccountName);
            
            var contacts = await _context.Contacts.ToListAsync();
            if (!contacts.Any(c => c.Email == requestDto.Email))
            {
                _context.Contacts.Add(new Contact()
                {
                    FirstName = requestDto.FirstName,
                    SecondName = requestDto.SecondName,
                    Email = requestDto.Email,
                    AccountId = account.Id,
                    Account = account
                });
                string IncName = StringGenerator.RandomStringGenerator();
                account.IncidentName = IncName;
                account.Incident = new Incident()
                {
                    IncidentName = IncName,
                    Description = requestDto.Description
                };
                _context.Entry(account).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
            }
            else
            {
                var contact = contacts.First(c => c.Email == requestDto.Email);
                contact.FirstName = requestDto.FirstName;
                contact.SecondName = requestDto.SecondName;
                if (contact.AccountId != account.Id)
                {
                    contact.AccountId = account.Id;
                    contact.Account = account;
                }
                _context.Entry(account).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            
            return Ok();
        }


        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
