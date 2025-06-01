using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.Categories.Commands;

public record EditCategoryCommand(
    Guid Id,
    string Name)
    : IRequest<Result<Category>>
{ }

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, Result<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public EditCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Category>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = request.Id,
            Name = request.Name
        };
        
        await _categoryRepository.UpdateAsync(category);

        return category;
    }
}