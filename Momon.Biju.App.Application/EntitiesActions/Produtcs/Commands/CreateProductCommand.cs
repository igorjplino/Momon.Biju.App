using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;

public record CreateProductCommand(
    string Name,
    string Price,
    Guid CategoryId)
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

        };
        
        return await _productRepository.CreateAsync(product);
    }
}