
using Scalar.AspNetCore;
using LuisEnmanuel_P1_P4.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IAutorService, AutorService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<IAutorService>().InitializeAsync();
}

app.MapOpenApi();

app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();