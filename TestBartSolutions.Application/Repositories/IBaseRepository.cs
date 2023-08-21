namespace TestBartSolutions.Application.Repositories;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task Update(int id, T item);
    Task Add(T item);
    Task Delete(int id);
}