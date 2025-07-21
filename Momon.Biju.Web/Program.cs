using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Momon.Biju.App.Application;
using Momon.Biju.App.Domain.Entities.Identity;
using Momon.Biju.App.Domain.Model;
using Momon.Biju.App.Infra;
using Momon.Biju.App.Infra.Contexts.Auth;
using Momon.Biju.Web.CookieManagers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddInfraServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();
builder.Services.AddScoped<CartCookieManager>();
builder.Services.AddScoped<FilterProductsCookieManager>();

builder.Services.Configure<Connections>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
        
builder.Services.ConfigureApplicationCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();
builder.Services.AddMvc()
    .AddRazorRuntimeCompilation();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.UseDefaultFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();