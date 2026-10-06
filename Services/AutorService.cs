using Dapper;
using LuisEnmanuel_P1_P4.Models;
using Microsoft.Data.Sqlite;

namespace LuisEnmanuel_P1_P4.Services;

public interface IAutorService
{
    Task InitializeAsync();
    Task<List<Autor>> ObtenerTodosAsync();
    Task<Autor?> ObtenerPorIdAsync(int id);
    Task<Autor> CrearAsync(Autor autor);
    Task<bool> ActualizarAsync(int id, Autor autor);
    Task<bool> EliminarAsync(int id);
}

public class AutorService(IConfiguration config) : IAutorService
{
    private readonly string _cs = config.GetConnectionString("DefaultConnection")!;

    public async Task InitializeAsync()
    {
        using var conn = new SqliteConnection(_cs);
        await conn.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Autores (
                IdAutor INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombres TEXT NOT NULL,
                Nacionalidad TEXT NOT NULL,
                FechaNacimiento TEXT NOT NULL,
                Sueldo REAL NOT NULL
            );");
    }

    public async Task<List<Autor>> ObtenerTodosAsync()
    {
        using var conn = new SqliteConnection(_cs);
        var autores = await conn.QueryAsync<Autor>("SELECT * FROM Autores");
        return autores.ToList();
    }

    public async Task<Autor?> ObtenerPorIdAsync(int id)
    {
        using var conn = new SqliteConnection(_cs);
        return await conn.QuerySingleOrDefaultAsync<Autor>(
            "SELECT * FROM Autores WHERE IdAutor = @id", new { id });
    }

    public async Task<Autor> CrearAsync(Autor autor)
    {
        using var conn = new SqliteConnection(_cs);
        var id = await conn.ExecuteScalarAsync<int>(@"
        INSERT INTO Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo)
        VALUES (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo);
        SELECT last_insert_rowid();", autor);
        return autor with { IdAutor = id };
    }

    public async Task<bool> ActualizarAsync(int id, Autor autor)
    {
        using var conn = new SqliteConnection(_cs);
        var filas = await conn.ExecuteAsync(@"
            UPDATE Autores
            SET Nombres = @Nombres, Nacionalidad = @Nacionalidad,
                FechaNacimiento = @FechaNacimiento, Sueldo = @Sueldo
            WHERE IdAutor = @id",
            new { id, autor.Nombres, autor.Nacionalidad, autor.FechaNacimiento, autor.Sueldo });
        return filas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        using var conn = new SqliteConnection(_cs);
        var filas = await conn.ExecuteAsync("DELETE FROM Autores WHERE IdAutor = @id", new { id });
        return filas > 0;
    }
}