using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Momon.Biju.App.Application.Common;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;

namespace Momon.Biju.App.Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        
        //TODO add validatiors

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddValidationBehavior<CreateProductCommand, Guid>();
            cfg.AddValidationBehavior<UpdateProductCommand, bool>();
        });
        
        return services;
    }
    
    private static void AddValidationBehavior<TRequest, TResponse>(this MediatRServiceConfiguration config) 
        where TRequest : notnull
    {
        config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationBehaviour<TRequest, TResponse>>();
    }
}