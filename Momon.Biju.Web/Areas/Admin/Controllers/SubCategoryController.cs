using MediatR;
using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Application.EntitiesActions.SubCategories.Commands;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Areas.Admin.Models;
using Momon.Biju.Web.Controllers;
using Momon.Biju.Web.Models;

namespace Momon.Biju.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class SubCategoryController : BaseController
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    
    public SubCategoryController(
        IMediator mediator,
        ISubCategoryRepository subCategoryRepository) 
        : base(mediator)
    {
        _subCategoryRepository = subCategoryRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var subCategories = await _subCategoryRepository.GetAllAsync();

        var vm = subCategories.Select(x => new SubCategoryViewModel
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
    public async Task<ActionResult> Create(CreateSubCategoryViewModel vm)
    {
        var command = new CreateSubCategoryCommand(vm.Name);
        
        await Mediator.Send(command);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<ActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var subCategory = await _subCategoryRepository.GetAsync(id.Value);

        if (subCategory is null)
        {
            return NotFound();
        }

        var vm = new EditSubCategoryViewModel
        {
            Id = subCategory.Id,
            Name = subCategory.Name
        };
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid id, EditSubCategoryViewModel vm)
    {
        var command = new EditSubCategoryCommand(
            vm.Id,
            vm.Name);
        
        await Mediator.Send(command);
        
        return RedirectToAction(nameof(Index));
    }
}