using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.SubCategories.Commands;

public record CreateSubCategoryCommand(
    string Name)
    : IRequest<Result<Guid>>
{ }

public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, Result<Guid>>
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public CreateSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<Result<Guid>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subCategory = new SubCategory
        {
            Name = request.Name
        };
        
        return await _subCategoryRepository.CreateAsync(subCategory);
    }
}