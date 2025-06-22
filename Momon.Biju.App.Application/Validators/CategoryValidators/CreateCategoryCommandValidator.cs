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
            .NotEmpty().WithMessage("Nome obrigatório")
            .MinimumLength(3).WithMessage("Mínimo de 3 caracteres")
            .MaximumLength(200).WithMessage("Máximo de 200 caracteres")
            .MustAsync(IsUniqueName).WithMessage("Nome cadastrado");
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken ct)
    {
        var category = await _categoryRepository.GetByNameAsync(name);

        return category is null;
    }
}