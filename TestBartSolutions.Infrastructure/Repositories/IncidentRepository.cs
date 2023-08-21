using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;
using TestBartSolutions.Infrastructure.DbContext;

namespace TestBartSolutions.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly APIContext _context;

    public IncidentRepository(APIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Incident>> GetAll()
    {
        return await _context.Incidents.ToListAsync();
    }

    public Task<Incident> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Incident> GetById(string id)
    {
        var incident = await _context.Incidents.FindAsync(id);

        return incident;
    }

    public Task Update(int id, Incident item)
    {
        throw new NotImplementedException();
    }

    public async Task Update(string id, Incident incident)
    {
        _context.Entry(incident).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    
    public async Task Add(Incident incident)
    {
        _context.Incidents.Add(incident);
        await _context.SaveChangesAsync();
    }


    public async Task Delete(int id)
    {
        var incident = await _context.Incidents.FindAsync(id);
        _context.Incidents.Remove(incident);
        await _context.SaveChangesAsync();

    }
    
}