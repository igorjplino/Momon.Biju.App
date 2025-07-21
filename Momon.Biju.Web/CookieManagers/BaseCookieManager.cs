using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

namespace Momon.Biju.Web.CookieManagers;

public abstract class BaseCookieManager<T> where T : class, new()
{
    private readonly string _cookieName;
    private readonly IDataProtector _protector;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly CookieOptions _defaultCookieOptions;

    protected BaseCookieManager(string cookieName,
        IHttpContextAccessor httpContextAccessor,
        IDataProtectionProvider dataProtectionProvider, 
        string purpose)
    {
        _cookieName = cookieName;
        HttpContextAccessor = httpContextAccessor;
        _protector = dataProtectionProvider.CreateProtector(purpose);

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        _defaultCookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(1),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
    }
    
    protected IHttpContextAccessor HttpContextAccessor { get; }

    public void Save(T value, int? expireTimeMinutes = null)
    {
        string json = JsonSerializer.Serialize(value, _jsonOptions);
        string protectedData = _protector.Protect(json); // Encrypt + encode

        if (expireTimeMinutes.HasValue)
        {
            _defaultCookieOptions.Expires = DateTimeOffset.Now.AddMinutes(expireTimeMinutes.Value);
        }
        
        HttpContextAccessor.HttpContext?.Response.Cookies.Append(_cookieName, protectedData, _defaultCookieOptions);
    }
    
    protected T? Get()
    {
        var cookieValue = HttpContextAccessor.HttpContext?.Request.Cookies[_cookieName];

        if (string.IsNullOrEmpty(cookieValue))
        {
            return null;
        }

        try
        {
            string decryptedJson = _protector.Unprotect(cookieValue); // Decode + decrypt
            return JsonSerializer.Deserialize<T>(decryptedJson, _jsonOptions);
        }
        catch
        {
            // TODO add error log
        }
        
        return null;
    }
    
    protected void Delete()
    {
        HttpContextAccessor.HttpContext?.Response.Cookies.Delete(_cookieName);
    }
}