using Microsoft.EntityFrameworkCore;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Infra.Contexts;

namespace Momon.Biju.App.Infra.Repositories;

public class SubCategoryRepository : BaseRepository<SubCategory>, ISubCategoryRepository
{
    public SubCategoryRepository(MomonBijuDbContext context) : base(context)
    {
    }

    public async Task<SubCategory?> GetByNameAsync(string name)
    {
        return await Context.SubCategories.FirstOrDefaultAsync(x => x.Name == name);
    }
}