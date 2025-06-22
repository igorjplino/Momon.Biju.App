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
            .NotEmpty().WithMessage("Nome obrigatório")
            .MinimumLength(3).WithMessage("Mínimo de 3 caracteres")
            .MaximumLength(200).WithMessage("Máximo de 200 caracteres")
            .MustAsync(IsUniqueName).WithMessage("Nome já cadastrado");
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken ct)
    {
        var subCategory = await _subCategoryRepository.GetByNameAsync(name);

        return subCategory is null;
    }
}