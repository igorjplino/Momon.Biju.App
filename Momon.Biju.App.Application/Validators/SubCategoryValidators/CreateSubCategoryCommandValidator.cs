using FluentValidation;
using Momon.Biju.App.Application.EntitiesActions.SubCategories.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators.SubCategoryValidators;

public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public CreateSubCategoryCommandValidator(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .MustAsync(IsUniqueName).WithMessage("Nome da subcategoria já cadastrada");
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken ct)
    {
        var subCategory = await _subCategoryRepository.GetByNameAsync(name);

        return subCategory is null;
    }
}