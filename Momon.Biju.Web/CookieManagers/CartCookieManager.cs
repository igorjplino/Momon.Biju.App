using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Momon.Biju.Web.Areas.Cart.Models;

namespace Momon.Biju.Web.CookieManagers;

public class CartCookieManager : BaseCookieManager<List<CartItem>>
{
    private const string CookieName = "ShopCart";
    private const string Purpose = "FilterProductsCookieManager.Protect";

    public CartCookieManager(
        IHttpContextAccessor httpContextAccessor,
        IDataProtectionProvider dataProtectionProvider)
        : base(CookieName, httpContextAccessor, dataProtectionProvider, Purpose)
    {
    }

    public List<CartItem> GetCart()
    {
        return Get() ?? [];
    }

    public void SaveCart(List<CartItem> value)
    {
        Delete();
        
        Save(value);
    }

    public void ClearCart()
    {
        Delete();
    }

    public int CartCount()
    {
        var cart = Get();
        
        return cart?.Sum(i => i.Quantity) ?? 0;
    }

    public decimal TotalPurchase()
    {
        var cart = Get();

        if (cart is null)
        {
            return 0;
        }
        
        return CartTotalPurchase(cart);
    }

    public decimal CartTotalPurchase(List<CartItem> items)
    {
        return items.Sum(x => x.Quantity * x.Price);
    }
}