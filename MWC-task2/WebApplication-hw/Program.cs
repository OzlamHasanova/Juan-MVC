using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//Services
builder.Services.AddControllersWithViews();
var connectionstr = builder.Configuration["ConnectionStrings: Default"];

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer("Server=DESKTOP-IPH870M;Database=JuanDb;Trusted_Connection=true");
});


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric=true;
    options.Password.RequireDigit=true;
    options.Password.RequireUppercase=true;
    options.Password.RequireLowercase=true;

    options.User.RequireUniqueEmail=true;

    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    options.Lockout.AllowedForNewUsers = false;
}).AddEntityFrameworkStores<AppDbContext>();



builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromSeconds(10);
});
var app = builder.Build();


//handle http request


app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();//login olmaq ucundur
app.UseAuthorization();//rollari yoxlayir(admin panele gire bilirsenmi ve ya update ede bilirsenmi)

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}"
    );

app.Run();
