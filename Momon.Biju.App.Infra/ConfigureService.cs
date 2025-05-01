using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Infra.Contexts;
using Momon.Biju.App.Infra.Repositories;

namespace Momon.Biju.App.Infra;

public static class ConfigureService
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MomonBijuDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(configuration.GetConnectionString("MomonBijuDb"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        return services;
    }
}