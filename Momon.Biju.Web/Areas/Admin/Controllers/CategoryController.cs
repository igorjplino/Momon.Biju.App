using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Application.EntitiesActions.Categories.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Areas.Admin.Models;
using Momon.Biju.Web.Controllers;
using Momon.Biju.Web.Helpers;

namespace Momon.Biju.Web.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class CategoryController : BaseController
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryController(
        IMediator mediator,
        ICategoryRepository categoryRepository)
        : base(mediator)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryRepository.GetAllAsync();

        var vm = categories.Select(x => new CategoryViewModel
        {
            Id = x.Id,
            Name = x.Name
        });
        
        return View(vm);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateCategoryViewModel vm)
    {
        var command = new CreateCategoryCommand(vm.Name);
        
        var result = await Mediator.Send(command);

        if (result.IsError)
        {
            ModelState.AddValidationException(result.Error);
            return View(vm);
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var category = await _categoryRepository.GetAsync(id.Value);

        if (category == null)
        {
            return NotFound();
        }

        var vm = new EditCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(EditCategoryViewModel vm)
    {
        var command = new EditCategoryCommand(
            vm.Id,
            vm.Name);
        
        var result = await Mediator.Send(command);
        
        if (result.IsError)
        {
            ModelState.AddValidationException(result.Error);
            return View(vm);
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet("ListCategories")]
    public async Task<IActionResult> ListCategories()
    {
        return Ok(await _categoryRepository.GetAllAsync());
    }
}