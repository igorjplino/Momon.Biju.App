using FluentValidation;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators.ProductValidators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    
    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .MustAsync(IsUniqueName).WithMessage("Nome do produto já cadastrado");
        
        RuleFor(x => x.Price)
            .NotEmpty()
            .SetValidator(new MustBeDecimalValidator());
    }
    
    private async Task<bool> IsUniqueName(string name, CancellationToken ct)
    {
        var product = await _productRepository.GetByNameAsync(name);

        return product is null;
    }
}