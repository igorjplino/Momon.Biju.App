using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Momon.Biju.Web.Dtos;
using Momon.Biju.Web.Dtos.Products;

namespace Momon.Biju.Web.CookieManagers;

public class FilterProductsCookieManager : BaseCookieManager<FilterProductsInListDto>
{
    private const string CookieName = "FilterProducts";
    private const string Purpose = "FilterProductsCookieManager.Protect";
    
    public FilterProductsCookieManager(
        IHttpContextAccessor httpContextAccessor,
        IDataProtectionProvider dataProtectionProvider) 
        : base(CookieName, httpContextAccessor, dataProtectionProvider, Purpose)
    { }

    public void SaveFilter(FilterProductsInListDto value)
    {
        Delete();
        
        Save(value);
    }
    
    public FilterProductsInListDto? GetFilters()
    {
        return Get();
    }
    
    public void DeleteFilter()
    {
        Delete();
    }
}