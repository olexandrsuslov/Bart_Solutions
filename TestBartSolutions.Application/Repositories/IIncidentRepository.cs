using TestBartSolutions.Core.Models;

namespace TestBartSolutions.Application.Repositories;

public interface IIncidentRepository : IBaseRepository<Incident>
{
    Task Update(string id, Incident incident);
    Task<Incident> GetById(string id);
}