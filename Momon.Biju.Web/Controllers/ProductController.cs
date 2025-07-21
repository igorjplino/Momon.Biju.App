using MediatR;
using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Queries;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Domain.Model;
using Momon.Biju.Web.Areas.Admin.Models;
using Momon.Biju.Web.Dtos;
using Momon.Biju.Web.Dtos.Products;
using Momon.Biju.Web.ViewModels.Products;
using X.PagedList;

namespace Momon.Biju.Web.Controllers;

public class ProductController : BaseController
{
    private readonly IProductRepository _productRepository;
    
    public ProductController(
        IMediator mediator,
        IProductRepository productRepository)
        : base(mediator)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IActionResult> Index([FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] FilterProductsInListDto? filters = null)
    {
        var query = new ListProductsQuery(new ProductFilters(
            pageNumber: pageNumber,
            pageSize: pageSize,
            name: filters?.Name,
            categoryId: filters?.SelectedCategoryId,
            subCategoryId: filters?.SelectedSubCategoryId
        ));
        
        var result = await Mediator.Send(query);
        
        var products = result.Value.Items //TODO handle all mediators results with error
            .Select(x => new ProductInListDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.ImagePath,
                Category = new CategoryViewModel
                {
                    Id = x.CategoryId,
                    Name = x.Category.Name
                }
            });
        
        var productsPaged  = new StaticPagedList<ProductInListDto>(products, result.Value.PageNumber, result.Value.PageSize, result.Value.Total);
        
        var vm = new ListProductsToOrderViewModel()
        {
            Products = productsPaged,
            PageSize = result.Value.PageSize,
            PageNumber = result.Value.PageNumber
        };

        return View(vm);
    }
    
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        
        var product = await _productRepository.GetToDetailsAsync(id.Value);

        if (product is null)
        {
            return NotFound();
        }
        
        var vm = new DetailsProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImagePath,
            CategoryName = product.Category.Name,
            SubCategories = product.SubCategories.Select(x => x.SubCategory.Name).ToList()
        };
        
        return View(vm);
    }
}