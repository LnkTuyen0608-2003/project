using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webbanhang_bai4.Models;
using webbanhang_bai4.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "Admin", pattern: "{area:exists}/{controller= Product}/{action = Index}/{id?}");
    endpoints.MapControllerRoute(name: "default", pattern: "{controller= Product}/{action = Index}/{id?}");
});
app.MapRazorPages();

app.Run();
