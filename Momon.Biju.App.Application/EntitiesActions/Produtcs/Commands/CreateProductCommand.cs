using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;

public record CreateProductCommand(
    string Name,
    string Description,
    string Price,
    string ReferenceNumber,
    bool Active,
    string ImagePath,
    Guid CategoryId,
    IEnumerable<Guid> SubCategories)
    : IRequest<Result<Guid>>
{ }

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = Math.Round(decimal.Parse(request.Price), 2),
            ReferenceNumber = request.ReferenceNumber,
            Active = request.Active,
            ImagePath = request.ImagePath,
            CategoryId = request.CategoryId,
            SubCategories = request.SubCategories.Select(x => new ProductSubCategory
            {
                SubCategoryId = x
            }).ToList()
        };
        
        return await _productRepository.CreateAsync(product);
    }
}