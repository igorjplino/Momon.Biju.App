using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Domain.Model;
using Momon.Biju.App.Infra.Contexts;

namespace Momon.Biju.App.Infra.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly string _momonBijuDb;

    public ProductRepository(
        MomonBijuDbContext context,
        IOptions<Connections> options)
        : base(context)
    {
        _momonBijuDb = options.Value.MomonBijuDb;
    }

    public async Task<(IEnumerable<Product> Exercises, int Total)> ListProductsAsync(ProductFilters filters)
    {
        var productsDict = new Dictionary<Guid, Product>();

        var sql =
            """
            SELECT
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.ImagePath,
                '' AS Category,
                c.Id,
                c.Name,
                COUNT(*) OVER() AS Total
            FROM 
                Products p
                INNER JOIN Categories c ON c.Id = p.CategoryId
            WHERE 1=1
            """;

        if (!string.IsNullOrWhiteSpace(filters.Name))
        {
            sql += " AND p.Name LIKE @Name";
        }

        sql +=
            """
            ORDER BY p.Name
            OFFSET @Offset ROWS 
            FETCH NEXT @PageSize ROWS ONLY
            """;

        var param = new
        {
            Offset = (filters.PageNumber - 1) * filters.PageSize,
            filters.PageSize,
            Name = $"%{filters.Name}%",
        };

        await using SqlConnection conn = new SqlConnection(_momonBijuDb);

        // IEnumerable<(Product Product, int Total)> result =
        var result =
            await conn.QueryAsync<Product, Category, int, (Product Product, int Total)>(
                sql: sql,
                param: param,
                splitOn: "Category,Total",
                map: (product, category, total) =>
                {
                    if (!productsDict.TryGetValue(product.Id, out Product productDict))
                    {
                        productsDict[product.Id] = product;
                    }
                    
                    productsDict[product.Id].Category = category;

                    return (productsDict[product.Id], total);
                });

        List<(Product Product, int Total)> valueTuples = result.ToList();

        var products = valueTuples.Select(x => x.Product);
        var total = valueTuples.FirstOrDefault().Total;

        return (products, total);
    }

    public Task<Product?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetToEditAsync(Guid id)
    {
        return await GetAsync(
            expression: x => x.Id == id,
            includes: source => source
                .Include(x => x.SubCategories));
    }

    public async Task UpdateProductAsync(Product product)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync();
        
        try
        {
            await Context.ProductsSubCategories
                .Where(x => x.ProductId == product.Id)
                .ExecuteDeleteAsync();

            await UpdateAsync(product);
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            //TODO log
            throw;
        }
    }
}