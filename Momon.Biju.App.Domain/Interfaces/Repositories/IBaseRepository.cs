using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.App.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<Guid> CreateAsync(T entity);
    Task<List<T>> CreateRangeAsync(List<T> entities);
    Task<List<T>> GetAllAsync();
    Task<T?> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(T entity);
}