using LuisEnmanuel_P1_P4.Data;
using LuisEnmanuel_P1_P4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LuisEnmanuel_P1_P4.Services;

public interface IAutorService
{
    Task<List<Autor>> ObtenerTodosAsync();
    Task<Autor?> ObtenerPorIdAsync(int id);
    Task<Autor> CrearAsync(Autor autor);
    Task<bool> ActualizarAsync(int id, Autor autor);
    Task<bool> EliminarAsync(int id);
}

public class AutorService(AppDbContext context) : IAutorService
{
    public async Task<List<Autor>> ObtenerTodosAsync()
    {
        return await context.Autores.ToListAsync();
    }

    public async Task<Autor?> ObtenerPorIdAsync(int id)
    {
        return await context.Autores.FindAsync(id);
    }

    public async Task<Autor> CrearAsync(Autor autor)
    {
        context.Autores.Add(autor);
        await context.SaveChangesAsync();
        return autor;
    }
    public async Task<bool> ActualizarAsync(int id, Autor autor)
    {
        var existente = await context.Autores.FindAsync(id);

        if (existente is null)
        {
            return false;
        }

        var actualizado = autor with { IdAutor = id };
        context.Entry(existente).CurrentValues.SetValues(actualizado);

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existente = await context.Autores.FindAsync(id);

        if (existente is null)
        {
            return false;
        }

        context.Autores.Remove(existente);
        await context.SaveChangesAsync();
        return true;
    }
}
