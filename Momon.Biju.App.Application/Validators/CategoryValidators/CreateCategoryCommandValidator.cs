using FluentValidation;
using Momon.Biju.App.Application.EntitiesActions.Categories.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators.CategoryValidators;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .MustAsync(IsUniqueName).WithMessage("Nome da categoria já cadastrada");
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken ct)
    {
        var category = await _categoryRepository.GetByNameAsync(name);

        return category is null;
    }
}