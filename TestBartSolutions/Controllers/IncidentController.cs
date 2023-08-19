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
    public class IncidentController : ControllerBase
    {
        private readonly AccountsContext _context;

        public IncidentController(AccountsContext context)
        {
            _context = context;
        }

        // GET: api/Incident
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents()
        {
          if (_context.Incidents == null)
          {
              return NotFound();
          }
            return await _context.Incidents.ToListAsync();
        }

        // GET: api/Incident/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncident(string id)
        {
          if (_context.Incidents == null)
          {
              return NotFound();
          }
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound();
            }

            return incident;
        }

        // PUT: api/Incident/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncident(string id, Incident incident)
        {
            if (id != incident.IncidentName)
            {
                return BadRequest();
            }

            _context.Entry(incident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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

        // POST: api/Incident
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Incident>> PostIncident(Incident incident)
        {
          if (_context.Incidents == null)
          {
              return Problem("Entity set 'AccountsContext.Incidents'  is null.");
          }
            _context.Incidents.Add(incident);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IncidentExists(incident.IncidentName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIncident", new { id = incident.IncidentName }, incident);
        }

        // DELETE: api/Incident/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(string id)
        {
            if (_context.Incidents == null)
            {
                return NotFound();
            }
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidentExists(string id)
        {
            return (_context.Incidents?.Any(e => e.IncidentName == id)).GetValueOrDefault();
        }
    }
}
