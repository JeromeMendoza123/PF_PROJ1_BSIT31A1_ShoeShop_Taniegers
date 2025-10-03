using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Repository;
using ShoeShop.Repository.Entities;
using ShoeShop.Services;
using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces;
using ShoeShop.Services.Services; // Make sure this points to your services folder

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Database contexts
builder.Services.AddDbContext<ShoeShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddDbContext<UserAccountDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserAccountConnection")!));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<UserAccountDbContext>()
.AddDefaultTokenProviders();

// Register your services for Dependency Injection
builder.Services.AddScoped<IShoeService, ShoeService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>(); // ? added
builder.Services.AddScoped<IPullOutService, PullOutService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<CartService>(); // Needed for HomeController

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
