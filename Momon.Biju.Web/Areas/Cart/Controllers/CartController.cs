using MediatR;
using Microsoft.AspNetCore.Mvc;
using Momon.Biju.App.Domain.Interfaces.Repositories;
using Momon.Biju.Web.Areas.Cart.Models;
using Momon.Biju.Web.Areas.Cart.Services;
using Momon.Biju.Web.Controllers;

namespace Momon.Biju.Web.Areas.Cart.Controllers;

[Area("Cart")]
public class CartController : BaseController
{
    private readonly IProductRepository _productRepository;
    
    public CartController(
        IMediator mediator,
        IProductRepository productRepository)
        : base(mediator)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var cart = CartCookieManager.GetCart(HttpContext);
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId, int? quantity)
    {
        var cart = CartCookieManager.GetCart(HttpContext);
        
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem is not null)
        {
            existingItem.Quantity += quantity ?? 1;
        }
        else
        {
            var product = await _productRepository.GetAsync(productId);

            if (product is null)
            {
                //TODO preparer the error message in different pages
                ModelState.AddModelError("ProductId", "Produto não encontrado");
                return RedirectToAction("Index");
            }
            
            cart.Add(new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Quantity = quantity ?? 1,
                Price = product.Price
            });
        }
        
        CartCookieManager.SaveCart(HttpContext, cart);
        
        return Json(new { success = true, cartCount = cart.Sum(i => i.Quantity) });
    }
    
    [HttpPost]
    public IActionResult RemoveFromCart(Guid productId)
    {
        var cart = CartCookieManager.GetCart(HttpContext);
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is not null)
        {
            cart.Remove(existingItem);
            CartCookieManager.SaveCart(HttpContext, cart);    
        }
        
        return Json(new { success = true, cartCount = cart.Sum(i => i.Quantity) });
    }
    
    [HttpPost]
    public IActionResult Clear()
    {
        CartCookieManager.ClearCart(HttpContext);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult CartCount()
    {
        var cart = CartCookieManager.GetCart(HttpContext);
        
        return Json(new { success = true, cartCount = cart.Sum(i => i.Quantity) });
    }
}