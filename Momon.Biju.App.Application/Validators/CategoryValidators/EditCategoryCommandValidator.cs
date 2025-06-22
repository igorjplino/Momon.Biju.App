using FluentValidation;
using Momon.Biju.App.Application.EntitiesActions.Categories.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators.CategoryValidators;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public EditCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome obrigatório")
            .MinimumLength(3).WithMessage("Mínimo de 3 caracteres")
            .MaximumLength(200).WithMessage("Máximo de 200 caracteres")
            .MustAsync(IsUniqueName).WithMessage("Nome cadastrado");
    }
    
    private async Task<bool> IsUniqueName(EditCategoryCommand command, string name, CancellationToken ct)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(name);

        return existingCategory is null || existingCategory.Id == command.Id;
    }
}