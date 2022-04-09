using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeGrow.Auth;

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
