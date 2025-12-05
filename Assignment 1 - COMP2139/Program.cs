using Assignment_1___COMP2139.Data;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Configure Serilog:
// ---------------------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ---------------------------
// Add services to the container:
// ---------------------------
builder.Services.AddControllersWithViews();

// ---------------------------
// Configure DbContext with PostgreSQL:
// ---------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// Configure Identity:
// ---------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ------------------------------
// Configure Cookie Paths (Login Redirect)
// ------------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// ---------------------------
// Authorization Policies:
// ---------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganizerOnly", policy =>
        policy.RequireRole("Organizer"));
});

// ---------------------------
// Razor Pages:
// ---------------------------
builder.Services.AddRazorPages();

var app = builder.Build();

// ---------------------------
// Seed Roles and Admin User on Startup:
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Seed roles
    await DbInitializer.SeedRoles(roleManager);

    // Seed admin user
    await DbInitializer.SeedAdminUser(userManager, roleManager);

// 🚀 Give Mackenzie's account the Organizer role
    var mackenzie = await userManager.FindByEmailAsync("macken@gmail.com"); // <-- change email to YOUR login email

    if (mackenzie != null && !(await userManager.IsInRoleAsync(mackenzie, "Organizer")))
    {
        await userManager.AddToRoleAsync(mackenzie, "Organizer");
    }

// ---------------------------
// Middleware Pipeline:
// ---------------------------
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/500"); // custom 500 error page
        app.UseStatusCodePagesWithReExecute("/Home/{0}"); // 404/other status codes
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication(); // Identity
    app.UseAuthorization();

// ---------------------------
// Map Routes & Razor Pages:
// ---------------------------
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

    app.Run();
}