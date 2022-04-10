using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WeGrow.Auth;
using WeGrow.Auth.Services;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{
    SeedData.EnsureSeedData(connString);
}

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
{
    options.UseSqlServer(connString,
        b => b.MigrationsAssembly(assembly));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddGoogle("Google", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.ClientId = "733138009138-5jc1v4isiomloiksb6asidkpo24qeajf.apps.googleusercontent.com";
        options.ClientSecret = "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"; 
    });
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
        {
            b.UseSqlServer(connString, opt => opt.MigrationsAssembly(assembly));
        };
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
        {
            b.UseSqlServer(connString, opt => opt.MigrationsAssembly(assembly));
        };
    })
    .AddDeveloperSigningCredential();

builder.Services.AddScoped<UserClaimsPrincipalFactory<IdentityUser>>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
