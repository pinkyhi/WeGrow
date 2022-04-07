using Microsoft.EntityFrameworkCore;
using WeGrow.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.MapControllers();

app.Run();