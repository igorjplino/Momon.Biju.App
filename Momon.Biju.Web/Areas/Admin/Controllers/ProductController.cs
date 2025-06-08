using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Queries;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Areas.Admin.Models;
using Momon.Biju.Web.Areas.Admin.Models.Products;
using Momon.Biju.Web.Controllers;
using Momon.Biju.Web.Helpers;

namespace Momon.Biju.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : BaseController
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public ProductController(
        IMediator mediator, 
        IProductRepository productRepository,
        ICategoryRepository categoryRepository, 
        ISubCategoryRepository subCategoryRepository)
        : base(mediator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
    }
    
    public async Task<IActionResult> Index(ListProductsQuery query)
    {
        var products = await Mediator.Send(query);

        var vm = new ListProductViewModel()
        {
            Products = products.Value.Items.Select(x => new ProductInListDto
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
            }),
            Filter = new FilterProductsInListDto(),
            PageNumber = products.Value.PageNumber,
            PageSize = products.Value.PageSize,
            Total = products.Value.Total
        };

        return View(vm);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var subcategories = await _subCategoryRepository.GetAllAsync();
    
        var vm = new CreateProductViewModel
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
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductViewModel vm)
    {
        var imagePath = await ImageUploadHelper.SaveProductImageAsync(vm.ProductImage);
        
        var command = new CreateProductCommand(
            vm.Name,
            vm.Description,
            vm.Price,
            vm.ReferenceNumber,
            imagePath,
            vm.SelectedCategoryId,
            vm.SelectedSubCategoriesId);
        
        await Mediator.Send(command);
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = await _productRepository.GetToEditAsync(id.Value);

        if (product == null)
        {
            return NotFound();
        }
        
        var categories = await _categoryRepository.GetAllAsync();
        var subcategories = await _subCategoryRepository.GetAllAsync();

        var vm = new EditProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price.ToString("F"),
            CurrentProductImage = product.ImagePath,
            SelectedCategoryId = product.CategoryId,
            SelectedSubCategoriesId = product.SubCategories.Select(x => x.SubCategoryId),
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

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(EditProductViewModel vm)
    // {
    //     
    // }
}