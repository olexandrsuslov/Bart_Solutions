using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Interfaces;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;
using TestBartSolutions.Models;


namespace TestBartSolutions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IRequestDto _requestDto;

        public AccountController(IAccountRepository repository, IRequestDto requestDto)
        {
            _repository = repository;
            _requestDto = requestDto;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _repository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }
            return new ActionResult<IEnumerable<Account>>(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var accounts = await _repository.GetAll();
            if (!accounts.Any())
            {
                return NotFound();
            }
            
            var account = await _repository.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(id,account);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            var accounts = await _repository.GetAll();
            if (!accounts.Any())
            {
                return Problem("Entity set 'APIContext.Contacts'  is null.");
            }
            
            _repository.Add(account);

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }
        
        [HttpPost("Dto")]
        public async Task<ActionResult> PostDto(RequestDto requestDto)
        {
            var accounts = await _repository.GetAll();
            Console.WriteLine(accounts);
            if (accounts.All(a => a.Name != requestDto.AccountName))
            {
                return NotFound();
            }

            await _requestDto.PostDto(requestDto);
            // var account = accounts.First(a => a.Name == requestDto.AccountName);
            //
            // var contacts = await _context.Contacts.ToListAsync();
            // if (!contacts.Any(c => c.Email == requestDto.Email))
            // {
            //     _context.Contacts.Add(new Contact()
            //     {
            //         FirstName = requestDto.FirstName,
            //         SecondName = requestDto.SecondName,
            //         Email = requestDto.Email,
            //         AccountId = account.Id,
            //         Account = account
            //     });
            //     string IncName = StringGenerator.RandomStringGenerator();
            //     account.IncidentName = IncName;
            //     account.Incident = new Incident()
            //     {
            //         IncidentName = IncName,
            //         Description = requestDto.Description
            //     };
            //     _context.Entry(account).State = EntityState.Modified;
            //     await _context.SaveChangesAsync();
            //     
            // }
            // else
            // {
            //     var contact = contacts.First(c => c.Email == requestDto.Email);
            //     contact.FirstName = requestDto.FirstName;
            //     contact.SecondName = requestDto.SecondName;
            //     if (contact.AccountId != account.Id)
            //     {
            //         contact.AccountId = account.Id;
            //         contact.Account = account;
            //     }
            //     _context.Entry(account).State = EntityState.Modified;
            //     await _context.SaveChangesAsync();
            // }
            //
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_repository.GetAll() == null)
            {
                return NotFound();
            }

            var account = await _repository.GetById(id);
            if (account == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return NoContent();
        }
    }
}
