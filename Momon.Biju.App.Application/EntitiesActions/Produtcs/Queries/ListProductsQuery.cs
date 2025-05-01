using MediatR;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Domain.Model;

namespace Momon.Biju.App.Application.EntitiesActions.Produtcs.Queries;

public record ListProductsQuery(
    ProductFilters? Filters)
    : IRequest<Result<PaginatedResult<Product>>>
{
}

public class GetAllExercisesQueryHandler : IRequestHandler<ListProductsQuery, Result<PaginatedResult<Product>>>
{
    private readonly IProductRepository _productRepository;

    public GetAllExercisesQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<PaginatedResult<Product>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var filters = request.Filters ?? new ProductFilters();
        
        (IEnumerable<Product> products, int total) = await _productRepository.ListProductsAsync(filters);

        return new PaginatedResult<Product>(
            filters.PageNumber,
            filters.PageSize,
            total,
            products.ToList().AsReadOnly());
    }
}