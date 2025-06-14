using System.Text;
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

        var sql = new StringBuilder(
            """
            WITH RankedProducts AS (
                SELECT
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.ImagePath,
                    c.Id AS CategoryId,
                    c.Name AS CategoryName,
                    ROW_NUMBER() OVER (PARTITION BY p.Id ORDER BY p.Name) AS rn
                FROM 
                    Products p
                    INNER JOIN ProductSubCategories psc ON psc.ProductId = p.Id
                    INNER JOIN Categories c ON c.Id = p.CategoryId
            """);

        var conditions = new List<string>();

        if (!string.IsNullOrWhiteSpace(filters.Name))
            conditions.Add("p.Name LIKE @Name");

        if (filters.CategoryId.HasValue)
            conditions.Add("c.Id = @CategoryId");

        if (filters.SubCategoryId.HasValue)
            conditions.Add("psc.SubCategoryId = @SubCategoryId");

        if (conditions.Count > 0)
            sql.AppendLine(" WHERE " + string.Join(" AND ", conditions));

        sql.AppendLine(
            """
            )
            SELECT
                Id,
                Name,
                Description,
                Price,
                ImagePath,
                CategoryId,
                CategoryName,
                COUNT(*) OVER() AS Total
            FROM RankedProducts
            WHERE rn = 1
            ORDER BY Name
            OFFSET @Offset ROWS 
            FETCH NEXT @PageSize ROWS ONLY;
            """);

        var param = new
        {
            Offset = (filters.PageNumber - 1) * filters.PageSize,
            filters.PageSize,
            Name = $"%{filters.Name}%",
            filters.CategoryId,
            filters.SubCategoryId,
        };

        await using var conn = new SqlConnection(_momonBijuDb);

        var result = await conn.QueryAsync<ProductRow>(
            sql: sql.ToString(),
            param: param);
        
        var products = result.Select(r => new Product
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Price = r.Price,
            ImagePath = r.ImagePath,
            Category = new Category
            {
                Id = r.CategoryId,
                Name = r.CategoryName
            }
        }).ToList();

        var total = result.FirstOrDefault()?.Total ?? 0;

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