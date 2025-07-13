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
        var cart = HttpContext.GetCart();

        var vm = new CartViewModel
        {
            Items = cart
        };
            
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId, int? quantity)
    {
        var cart = HttpContext.GetCart();
        
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
        
        HttpContext.SaveCart(cart);
        
        return Json(new { success = true, cartCount = cart.Sum(i => i.Quantity) });
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateCart(Guid productId, int quantity)
    {
        var cart = HttpContext.GetCart();
        
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;

            if (existingItem.Quantity <= 0)
            {
                cart.Remove(existingItem);
            }
        }
        else
        {
            var product = await _productRepository.GetAsync(productId);

            if (product is null)
            {
                return NotFound();
            }
            
            cart.Add(new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Quantity = quantity,
                Price = product.Price
            });
        }
        
        HttpContext.SaveCart(cart);

        return PartialView("_CartItemsList", cart);
    }
    
    [HttpPost]
    public IActionResult UpdateProductQuantity(Guid productId, int change)
    {
        var cart = HttpContext.GetCart();
        
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is null)
        {
            return NotFound();
        }
        
        existingItem.Quantity += change;
        
        HttpContext.SaveCart(cart);

        return PartialView("_CartItemsList", cart);
    }
    
    [HttpPost]
    public IActionResult RemoveFromCart(Guid productId)
    {
        var cart = HttpContext.GetCart();
        var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is not null)
        {
            cart.Remove(existingItem);
            HttpContext.SaveCart(cart);
        }
        
        return PartialView("_CartItemsList", cart);
    }
    
    public IActionResult Clear()
    {
        CartCookieManager.ClearCart(HttpContext);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult CartCount()
    {
        return Json(new { success = true, cartCount = HttpContext.CartCount() });
    }
    
    [HttpGet]
    public IActionResult CartPurchaseTotal()
    {
        return Json(new { success = true, cartPurchaseTotal = HttpContext.TotalPurchase().ToString("C") });
    }
}