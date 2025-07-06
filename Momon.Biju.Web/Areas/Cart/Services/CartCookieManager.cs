using System.Text.Json;
using Momon.Biju.Web.Areas.Cart.Models;

namespace Momon.Biju.Web.Areas.Cart.Services;

public class CartCookieManager
{
    private const string CookieName = "ShopCart";

    public static List<CartItem> GetCart(HttpContext context)
    {
        var cookie = context.Request.Cookies[CookieName];
        
        if (string.IsNullOrEmpty(cookie))
            return [];

        return JsonSerializer.Deserialize<List<CartItem>>(cookie) ?? [];
    }

    public static void SaveCart(HttpContext context, List<CartItem> cart)
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

    public static void ClearCart(HttpContext context)
    {
        context.Response.Cookies.Delete(CookieName);
    }
}