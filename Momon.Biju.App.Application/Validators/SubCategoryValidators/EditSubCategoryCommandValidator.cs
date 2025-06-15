using FluentValidation;
using Momon.Biju.App.Application.EntitiesActions.SubCategories.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators.SubCategoryValidators;

public class EditSubCategoryCommandValidator : AbstractValidator<EditSubCategoryCommand>
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public EditSubCategoryCommandValidator(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .MustAsync(IsUniqueName).WithMessage("Nome da subcategoria já cadastrada");
    }
    
    private async Task<bool> IsUniqueName(EditSubCategoryCommand command, string name, CancellationToken ct)
    {
        var existingSubCategory = await _subCategoryRepository.GetByNameAsync(name);

        return existingSubCategory is null || existingSubCategory.Id == command.Id;
    }
}