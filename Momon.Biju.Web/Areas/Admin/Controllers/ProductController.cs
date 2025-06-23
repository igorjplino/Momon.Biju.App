using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Commands;
using Momon.Biju.App.Application.EntitiesActions.Produtcs.Queries;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.App.Domain.Model;
using Momon.Biju.Web.Areas.Admin.Models;
using Momon.Biju.Web.Areas.Admin.Models.Products;
using Momon.Biju.Web.Controllers;
using Momon.Biju.Web.Helpers;
using X.PagedList;

namespace Momon.Biju.Web.Areas.Admin.Controllers;

[Authorize]
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
    
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] FilterProductsInListDto? filters)
    {
        var query = new ListProductsQuery(new ProductFilters(
            pageNumber: filters?.PageNumber,
            pageSize: filters?.PageSize,
            name: filters?.Name,
            categoryId: filters?.SelectedCategoryId,
            subCategoryId: filters?.SelectedSubCategoryId
        ));
        
        var result = await Mediator.Send(query);
        
        var categories = await _categoryRepository.GetAllAsync();
        var subcategories = await _subCategoryRepository.GetAllAsync();

        var products = result.Value.Items
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
        
        var vm = new ListProductViewModel()
        {
            Products = productsPaged,
            Filter = new FilterProductsInListDto
            {
                SelectedCategoryId = query.Filters?.CategoryId,
                SelectedSubCategoryId = query.Filters?.SubCategoryId,
                Categories = categories.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),
                SubCategories = subcategories.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),
                PageSize = result.Value.PageSize,
                PageNumber = result.Value.PageNumber
            }
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
            CategoriesToSelect = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }),
            SubCategoriesToSelect = subcategories.Select(x => new SelectListItem
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
        var imagePath = await ImageUploadHelper.SaveProductImageAsync(vm.ImagePath);
        
        var command = new CreateProductCommand(
            vm.Name,
            vm.Description,
            vm.Price,
            vm.ReferenceNumber,
            imagePath,
            vm.CategoryId,
            vm.SubCategories);
        
        var result = await Mediator.Send(command);
        
        if (result.IsError)
        {
            ModelState.AddValidationException(result.Error);
            
            var categories = await _categoryRepository.GetAllAsync();
            var subcategories = await _subCategoryRepository.GetAllAsync();

            vm.CategoriesToSelect = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            vm.SubCategoriesToSelect = subcategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            
            return View(vm);
        }
        
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
            Price = product.Price,
            ReferenceNumber = product.ReferenceNumber,
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProductViewModel vm)
    {
        var imagePath = await ImageUploadHelper.SaveProductImageAsync(vm.NewProductImage);
        
        var command = new EditProductCommand(
            vm.Id,
            vm.Name,
            vm.Description,
            vm.Price,
            vm.ReferenceNumber,
            imagePath ?? vm.CurrentProductImage,
            vm.SelectedCategoryId,
            vm.SelectedSubCategoriesId);
        
        var result = await Mediator.Send(command);
        
        if (result.IsError)
        {
            ModelState.AddValidationException(result.Error);
            return View(vm);
        }
        
        return RedirectToAction("Index");
    }
}