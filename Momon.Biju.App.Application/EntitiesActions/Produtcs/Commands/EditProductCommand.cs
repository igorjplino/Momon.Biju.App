using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;

public record EditProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    string ReferenceNumber,
    bool Active,
    string ImagePath,
    Guid CategoryId,
    IEnumerable<Guid> SubCategories)
    : IRequest<Result<Product>>
{
}

public class UpdateProductCommandHandler : IRequestHandler<EditProductCommand, Result<Product>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.ProductId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ReferenceNumber = request.ReferenceNumber,
            Active = request.Active,
            ImagePath = request.ImagePath,
            CategoryId = request.CategoryId,
            SubCategories = request.SubCategories.Select(x => new ProductSubCategory
            {
                SubCategoryId = x
            }).ToList()
        };
        
        await _productRepository.UpdateProductAsync(product);
        
        return product;
    }
}