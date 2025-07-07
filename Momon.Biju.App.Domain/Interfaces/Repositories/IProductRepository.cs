using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Model;

namespace Momon.Biju.App.Domain.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<(IEnumerable<Product> Exercises, int Total)> ListProductsAsync(ProductFilters filters);
    Task<Product?> GetByNameAsync(string name);
    Task<Product?> GetToEditAsync(Guid id);
    Task<Product?> GetToDetailsAsync(Guid id);
    Task UpdateProductAsync(Product product);
}