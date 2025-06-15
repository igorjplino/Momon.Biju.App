using Momon.Biju.App.Domain.Entities;

namespace Momon.Biju.App.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}