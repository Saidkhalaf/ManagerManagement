using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.DAL;
using ManagerManagement.DAL.EF;
using ManagerManagement.UI.MVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ManagerDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ManagerDbContext>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();
builder.Services.AddControllers().AddXmlSerializerFormatters();

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.Events.OnRedirectToLogin += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 401;
        }

        return Task.CompletedTask;
    };

    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 401; //Als de gebruiker niet is ingelogd moet er steeds 401 teruggegeven worden.
        }

        return Task.CompletedTask;
    };
    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 403;
        }

        return Task.CompletedTask;
    };
});

void ConfigureServices(IServiceCollection services)
{

    services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdminRole", policy =>
        {
            policy.RequireRole("Admin");
        });

    });
    
}


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ManagerDbContext>();
    if (context.CreateDatabase(dropDatabase:true))
    {
        //Add users 
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();
        
        SeedIdentity(userManager, roleManager);
        DataSeeder.Seed(context);
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapRazorPages();

app.Run();

void SeedIdentity(UserManager<IdentityUser> userManager, 
    RoleManager<IdentityRole> roleManager)
{
    var userRole = new IdentityRole(CustomIdentityConstants.UserRole);
    roleManager.CreateAsync(userRole).Wait();
    
    var adminRole = new IdentityRole(CustomIdentityConstants.AdminRole);
    roleManager.CreateAsync(adminRole).Wait();
    
    var sami = new IdentityUser
    {
        UserName = "sami@kdg.be",
        Email = "sami@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(sami, "Password1!").Wait();
    userManager.AddToRoleAsync(sami, CustomIdentityConstants.UserRole).Wait();
    
    var said = new IdentityUser
    {
        UserName = "said@kdg.be",
        Email = "said@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(said, "Password1!").Wait();
    userManager.AddToRoleAsync(said, CustomIdentityConstants.AdminRole).Wait();
    
    
    var jan = new IdentityUser
    {
        UserName = "jan@kdg.be",
        Email = "jan@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(jan, "Password1!").Wait();
    userManager.AddToRoleAsync(jan, CustomIdentityConstants.UserRole).Wait();
    
    var peter = new IdentityUser
    {
        UserName = "peter@kdg.be",
        Email = "peter@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(peter, "Password1!").Wait();
    userManager.AddToRoleAsync(peter, CustomIdentityConstants.UserRole).Wait();
    
    var tobias = new IdentityUser
    {
        UserName = "tobias@kdg.be",
        Email = "tobias@kdg.be",
        EmailConfirmed = true
    };
    userManager.CreateAsync(tobias, "Password1!").Wait();
    userManager.AddToRoleAsync(tobias, CustomIdentityConstants.UserRole).Wait();
}

public partial class Program { }