using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WeGrow.Extensions;
using WeGrow.LiqPay.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtSwagger();
builder.Services.AddControllers();
builder.Services.AddAutoMapper();
builder.Services.AddLiqPay();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", p => p.RequireScope("WeGrow.admin"));
});
builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.Authority = builder.Configuration["authUrl"];
        options.ApiName = "WeGrow";

    });
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();
app.UseSwagger();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI();

app.Run();