using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.Categories.Queries;

public record ListCategoriesQuery()
    : IRequest<Result<List<Category>>>
{ }

public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, Result<List<Category>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<List<Category>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
    {
        return (await _categoryRepository.GetAllAsync()).ToList();
    }
}