using Microsoft.EntityFrameworkCore;
using WeGrow.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.Authority = builder.Configuration["apiUrl"];
        options.ApiName = "WeGrow";

    });
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();