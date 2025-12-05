using Assignment_1___COMP2139.Data;
using Assignment_1___COMP2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------
// Serilog Logging
// ----------------------------------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ----------------------------------------
// Add Services
// ----------------------------------------
builder.Services.AddControllersWithViews();

// PostgreSQL Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cookie redirect paths
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

// Role-based authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganizerOnly", policy =>
        policy.RequireRole("Organizer"));
});

builder.Services.AddRazorPages();

var app = builder.Build();

// ----------------------------------------
// Apply Migrations & Seed Database
// ----------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations
        context.Database.Migrate();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // Seed roles and default admin user
        await DbInitializer.SeedRoles(roleManager);
        await DbInitializer.SeedAdminUser(userManager, roleManager);

        // Promote Mackenzie to Organizer
        var mackenzie = await userManager.FindByEmailAsync("macken@gmail.com");
        if (mackenzie != null && !await userManager.IsInRoleAsync(mackenzie, "Organizer"))
        {
            await userManager.AddToRoleAsync(mackenzie, "Organizer");
        }

        // Promote Kelly to Admin
        var kelly = await userManager.FindByEmailAsync("kelly@gmail.com");
        if (kelly != null && !await userManager.IsInRoleAsync(kelly, "Admin"))
        {
            await userManager.AddToRoleAsync(kelly, "Admin");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while migrating or seeding the database.");
        throw;
    }
}

// ----------------------------------------
// Middleware Pipeline
// ----------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/500");
    app.UseStatusCodePagesWithReExecute("/Home/Error{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// MVC + Razor Pages routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();