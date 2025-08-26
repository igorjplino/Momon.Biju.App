using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Application.EntitiesActions.Categories.Commands;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Application.EntitiesActions.Shop.Commands;
using Momon.Biju.App.Application.EntitiesActions.SubCategories.Commands;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Model;

namespace Momon.Biju.App.Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Validation stops on the first condition that is false for each property
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddValidationBehavior<CreateProductCommand, Guid>();
            cfg.AddValidationBehavior<EditProductCommand, Product>();
            
            cfg.AddValidationBehavior<CreateCategoryCommand, Guid>();
            
            cfg.AddValidationBehavior<CreateSubCategoryCommand, Guid>();
            
            cfg.AddValidationBehavior<FinishPurchaseCommand, OrderRequest>();
        });
        
        return services;
    }
    
    private static void AddValidationBehavior<TRequest, TResponse>(this MediatRServiceConfiguration config) 
        where TRequest : notnull
    {
        config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationBehaviour<TRequest, TResponse>>();
    }
}