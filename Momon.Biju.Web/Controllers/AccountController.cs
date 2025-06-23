using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Domain.Entities.Identity;
using Momon.Biju.Web.Models.Account;

namespace Momon.Biju.Web.Controllers;

public class AccountController : BaseController
{
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(
        IMediator mediator,
        SignInManager<AppUser> signInManager) : base(mediator)
    {
        _signInManager = signInManager;
    }

    public IActionResult Login(string? returnUrl = null)
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(vm);

        var result = await _signInManager.PasswordSignInAsync(
            vm.Email,
            vm.Password,
            isPersistent: true,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        ModelState.AddModelError(string.Empty, "Login inválido.");
        return View(vm);
    }
    
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = new AppUser
        {
            DisplayName = vm.Name,
            UserName = vm.Email
        };
        
        var result = await _signInManager.UserManager.CreateAsync(user, vm.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        
        return View(vm);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}