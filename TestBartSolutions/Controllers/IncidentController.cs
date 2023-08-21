using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;


namespace TestBartSolutions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository _repository;

        public IncidentController(IIncidentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents()
        {
            var incidents = await _repository.GetAll();
            if (!incidents.Any())
            {
                return NotFound();
            }
            return new ActionResult<IEnumerable<Incident>>(incidents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncident(string id)
        {
            var incidents = await _repository.GetAll();
            if (!incidents.Any())
            {
                return NotFound();
            }
            
            var incident = await _repository.GetById(id);

            if (incident == null)
            {
                return NotFound();
            }

            return incident;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, Incident incident)
        {
            if (id != incident.IncidentName)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(id,incident);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Incident>> PostIncident(Incident incident)
        {
            var incidents = await _repository.GetAll();
            if (!incidents.Any())
            {
                return Problem("Entity set 'APIContext.Incidents'  is null.");
            }
            
            _repository.Add(incident);

            return CreatedAtAction("GetIncident", new { id = incident.IncidentName }, incident);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(int id)
        {
            if (_repository.GetAll() == null)
            {
                return NotFound();
            }

            var incident = await _repository.GetById(id);
            if (incident == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return NoContent();
        }
    }
}
