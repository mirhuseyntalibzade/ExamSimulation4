using BL.Profiles;
using BL.Services.Abstractions;
using BL.Services.Concretes;
using DAL.Contexts;
using DAL.Repository.Absractions;
using DAL.Repository.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(DoctorProfile));
builder.Services.AddAutoMapper(typeof(DepartmentProfile));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>policy.RequireRole("Admin"));
    options.AddPolicy("User", policy =>policy.RequireRole("User"));
});

builder.Services.ConfigureApplicationCookie(options =>
{

    options.LoginPath = "/Account/Login"; 
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/login";
});

builder.Services.AddAuthentication().AddCookie();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        await DatabaseSeeder.SeedData(scope.ServiceProvider);
    }catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}"
);

app.Run();
