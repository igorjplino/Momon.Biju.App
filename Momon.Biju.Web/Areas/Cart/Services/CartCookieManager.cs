using System.Text.Json;
using Momon.Biju.Web.Areas.Cart.Models;

namespace Momon.Biju.Web.Areas.Cart.Services;

public static class CartCookieManager
{
    private const string CookieName = "ShopCart";

    public static List<CartItem> GetCart(this HttpContext context)
    {
        var cookie = context.Request.Cookies[CookieName];
        
        if (string.IsNullOrEmpty(cookie))
            return [];

        return JsonSerializer.Deserialize<List<CartItem>>(cookie) ?? [];
    }

    public static void SaveCart(this HttpContext context, List<CartItem> cart)
    {
        var options = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(2),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
        
        var json = JsonSerializer.Serialize(cart);
        
        context.Response.Cookies.Append(CookieName, json, options);
    }

    public static void ClearCart(this HttpContext context)
    {
        context.Response.Cookies.Delete(CookieName);
    }

    public static int CartCount(this HttpContext context)
    {
        var cart = GetCart(context);
        
        return cart.Sum(i => i.Quantity);
    }

    public static decimal TotalPurchase(this HttpContext context)
    {
        var cart = GetCart(context);
        
        return cart.CartTotalPurchase();
    }

    public static decimal CartTotalPurchase(this List<CartItem> items)
    {
        return items.Sum(x => x.Quantity * x.Price);
    }
}