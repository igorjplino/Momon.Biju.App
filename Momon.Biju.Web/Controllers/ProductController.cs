using MediatR;
using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Queries;
using Momon.Biju.Web.Models;

namespace Momon.Biju.Web.Controllers;

public class ProductController : BaseController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<ActionResult> Index(ListProductsQuery query)
    {
        var products = await Mediator.Send(query);

        var vm = new ListProductViewModel
        {
            Products = products.Value.Items.Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                QuantityInStock = 0,
                Category = new CategoryViewModel
                {
                    Id = x.CategoryId,
                    Name = x.Category.Name
                }
            }).ToList(),
            Filters = new ProductFilter(),
            PageNumber = products.Value.PageNumber,
            PageSize = products.Value.PageSize,
            Total = products.Value.Total
        };

        return View(vm);
    }
    
    public async Task<ActionResult> Filter(ListProductsQuery query)
    {
        var products = await Mediator.Send(query);

        var vm = products.Value?.Items.Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            QuantityInStock = 0,
            Category = new CategoryViewModel
            {
                Id = x.CategoryId,
                Name = x.Category.Name
            }
        }).ToList();

        return PartialView("_ProductList", vm);
    }

    public ActionResult NewProduct()
    {
        return View();
    }

    public async Task<ActionResult> NewProduct(NewProductViewModel vm)
    {
        var command = new CreateProductCommand(
            vm.Name,
            vm.Price,
            vm.CategoryId);
        
        await Mediator.Send(command);
        
        return RedirectToAction("Index");
    }
}