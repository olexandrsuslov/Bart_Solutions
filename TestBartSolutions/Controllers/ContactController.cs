
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;


namespace TestBartSolutions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _repository.GetAll();
            if (!contacts.Any())
            {
                return NotFound();
            }
            return new ActionResult<IEnumerable<Contact>>(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contacts = await _repository.GetAll();
            if (!contacts.Any())
            {
                return NotFound();
            }
            
            var contact = await _repository.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(id,contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            var contacts = await _repository.GetAll();
            if (!contacts.Any())
            {
                return Problem("Entity set 'APIContext.Contacts'  is null.");
            }
            
            _repository.Add(contact);

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_repository.GetAll() == null)
            {
                return NotFound();
            }

            var contact = await _repository.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return NoContent();
        }

    }
}
