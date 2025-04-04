using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapNotaEndpoints();

app.Run();