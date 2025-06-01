using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Queries;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Helpers;
using Momon.Biju.Web.Models;

namespace Momon.Biju.Web.Controllers;

public class ProductController : BaseController
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public ProductController(
        IMediator mediator,
        ICategoryRepository categoryRepository, 
        IProductRepository productRepository, 
        ISubCategoryRepository subCategoryRepository)
        : base(mediator)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<IActionResult> Index(ListProductsQuery query)
    {
        var products = await Mediator.Send(query);

        var vm = new ListProductViewModel
        {
            Products = products.Value.Items.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = "/images/products/anel.jpg",
                Category = new CategoryViewModel
                {
                    Id = x.CategoryId,
                    Name = x.Category.Name
                }
            }).ToList(),
            FiltersDto = new ProductFilterDto(),
            PageNumber = products.Value.PageNumber,
            PageSize = products.Value.PageSize,
            Total = products.Value.Total
        };

        return View(vm);
    }
    
    public async Task<IActionResult> Filter(ListProductsQuery query)
    {
        var products = await Mediator.Send(query);

        var vm = products.Value?.Items.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Category = new CategoryViewModel
            {
                Id = x.CategoryId,
                Name = x.Category.Name
            }
        }).ToList();

        return PartialView("_ProductList", vm);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var categories = await _categoryRepository.GetAllAsync();
        // var subcategories = await _subCategoryRepository.GetAllAsync();

        var vm = new NewProductViewModel
        {
            Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            // SubCategories = subcategories.Select(x => new SelectListItem
            // {
            //     Value = x.Id.ToString(),
            //     Text = x.Name
            // })
        };
        
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(NewProductViewModel vm, IFormFile productImage)
    {
        var imagePath = await ImageUploadHelper.SaveProductImageAsync(productImage);
        
        var command = new CreateProductCommand(
            vm.Name,
            vm.Description,
            vm.Price,
            vm.SelectedCategoryId,
            vm.SelectedSubCategoriesId);
        
        await Mediator.Send(command);
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        var product = await _productRepository.GetAsync(id);

        if (product is null)
        {
            return NotFound();
        }
        
        var categories = await _categoryRepository.GetAllAsync();
        var subcategories = await _subCategoryRepository.GetAllAsync();
        
        var vm = new UpdateProductViewModel(product)
        {
            Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }),
            SubCategories = subcategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
        };
        
        return View(vm);
    }
    
    public async Task<IActionResult> UpdateProduct(UpdateProductViewModel vm)
    {
        var command = new UpdateProductCommand(
            vm.Id,
            vm.Name,
            vm.Price,
            vm.SelectedCategoryId,
            vm.SelectedSubCategoriesId
        );
        
        await Mediator.Send(command);
        
        return View(vm);
    }
}