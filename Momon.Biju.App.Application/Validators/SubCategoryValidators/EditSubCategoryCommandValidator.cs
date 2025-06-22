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
            .NotEmpty().WithMessage("Nome obrigatório")
            .MinimumLength(3).WithMessage("Mínimo de 3 caracteres")
            .MaximumLength(200).WithMessage("Máximo de 200 caracteres")
            .MustAsync(IsUniqueName).WithMessage("Nome cadastrado");
    }
    
    private async Task<bool> IsUniqueName(EditSubCategoryCommand command, string name, CancellationToken ct)
    {
        var existingSubCategory = await _subCategoryRepository.GetByNameAsync(name);

        return existingSubCategory is null || existingSubCategory.Id == command.Id;
    }
}