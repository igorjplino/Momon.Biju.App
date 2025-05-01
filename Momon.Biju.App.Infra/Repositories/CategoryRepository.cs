using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Infra.Contexts;

namespace Momon.Biju.App.Infra.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MomonBijuDbContext context) : base(context)
    {
    }
}