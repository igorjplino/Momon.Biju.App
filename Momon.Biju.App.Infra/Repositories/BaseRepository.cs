using Microsoft.EntityFrameworkCore;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Infra.Contexts;

namespace Momon.Biju.App.Infra.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected MomonBijuDbContext Context { get; }

    public BaseRepository(MomonBijuDbContext context)
    {
        Context = context;
    }

    public async Task<Guid> CreateAsync(T entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<List<T>> CreateRangeAsync(List<T> entities)
    {
        await Context.AddRangeAsync(entities);
        await Context.SaveChangesAsync();

        return entities;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(Guid id)
    {
        return await Context.FindAsync<T>(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Context.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync();
    }
    
    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }
}