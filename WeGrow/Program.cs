using Microsoft.EntityFrameworkCore;
using WeGrow.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 
var app = builder.Build();

app.Run();