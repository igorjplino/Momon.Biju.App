using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.SubCategories.Commands;

public record EditSubCategoryCommand(
    Guid Id,
    string Name)
    : IRequest<Result<SubCategory>>
{ }

public class EditSubCategoryCommandHandler : IRequestHandler<EditSubCategoryCommand, Result<SubCategory>>
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public EditSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<Result<SubCategory>> Handle(EditSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subCategory = new SubCategory
        {
            Id = request.Id,
            Name = request.Name
        };
        
        await _subCategoryRepository.UpdateAsync(subCategory);
        
        return subCategory;
    }
}