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
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .MustAsync(IsUniqueName).WithMessage("Nome da categoria já cadastrada");
    }
    
    private async Task<bool> IsUniqueName(EditCategoryCommand command, string name, CancellationToken ct)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(name);

        return existingCategory is null || existingCategory.Id == command.Id;
    }
}