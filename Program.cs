using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using LuisEnmanuel_P1_P4.Data;
using LuisEnmanuel_P1_P4.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAutorService, AutorService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapOpenApi();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();