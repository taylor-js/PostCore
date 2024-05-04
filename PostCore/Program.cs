using PostCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("HerokuPostgres");
builder.Services.AddDbContext<D2glkvqrc1vuvsContext>(options => options.UseNpgsql(connectionString));

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

app.UseAuthorization();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

app.MapControllerRoute(
    name: "assetMgmt",
    pattern: "", // Empty pattern for default route
    defaults: new { controller = "AssetMgmt", action = "Index" });

app.MapControllerRoute(
    name: "aMContent",
    pattern: "AMContent/{action}/{id?}/{c_id?}",
    defaults: new { controller = "AMContent", action = "Index" });

app.MapControllerRoute(
    name: "aMDistrib",
    pattern: "AMDistrib/{action}/{id?}/{d_id?}",
    defaults: new { controller = "AMDistrib", action = "Index" });

// Default route for HomeController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
