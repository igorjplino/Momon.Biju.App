using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;

public record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Price,
    Guid CategoryId,
    IEnumerable<Guid> SubCategories)
    : IRequest<Result<Product>>
{
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Product>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.ProductId,
            Name = request.Name,
            Price = Math.Round(decimal.Parse(request.Price), 2),
            CategoryId = request.CategoryId,
            SubCategories = request.SubCategories.Select(x => new ProductSubCategory
            {
                SubCategoryId = x
            }).ToList()
        };
        
        await _productRepository.UpdateAsync(product);
        
        return product;
    }
}