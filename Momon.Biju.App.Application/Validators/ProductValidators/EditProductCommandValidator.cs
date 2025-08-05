using FluentValidation;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators.ProductValidators;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    private readonly IProductRepository _productRepository;
    
    public EditProductCommandValidator(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISubCategoryRepository subCategoryRepository)
    {
        _productRepository = productRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome obrigatório")
            .MinimumLength(3).WithMessage("Mínimo de 3 caracteres")
            .MaximumLength(200).WithMessage("Máximo de 200 caracteres")
            .MustAsync(IsUniqueName).WithMessage("Nome do produto já cadastrado");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Nome obrigatório")
            .MinimumLength(3).WithMessage("Mínimo de 3 caracteres")
            .MaximumLength(500).WithMessage("Máximo de 500 caracteres");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Preço obrigatório");

        RuleFor(x => x.ImagePath)
            .NotEmpty().WithMessage("Imagem obrigatória")
            .MaximumLength(2000).WithMessage("Máximo de 2000 caracteres");
        
        RuleFor(x => x.ReferenceNumber)
            .NotEmpty().WithMessage("Número de referência obrigatório")
            .MaximumLength(100).WithMessage("Máximo de 100 caracteres");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Categoria obrigatória")
            .SetValidator(new EntityMustExistsValidator<Category>(categoryRepository));

        RuleFor(x => x.SubCategories)
            .NotEmpty().WithMessage("Selecionar ao menos uma subcategoria");
        
        RuleForEach(x => x.SubCategories)
            .SetValidator(new EntityMustExistsValidator<SubCategory>(subCategoryRepository)).WithMessage("Subcategorias inválidas: {PropertyName}");
    }
    
    private async Task<bool> IsUniqueName(EditProductCommand command, string name, CancellationToken ct)
    {
        var product = await _productRepository.GetByNameAsync(name);

        return product is null || command.ProductId == product.Id;
    }
}