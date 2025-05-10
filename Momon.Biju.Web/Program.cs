using System.Globalization;
using FluentValidation;
using Momon.Biju.App.Application;
using Momon.Biju.App.Domain.Model;
using Momon.Biju.App.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddInfraServices(builder.Configuration);

builder.Services.Configure<Connections>(builder.Configuration.GetSection("ConnectionStrings"));

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
app.UseRouting();

app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();