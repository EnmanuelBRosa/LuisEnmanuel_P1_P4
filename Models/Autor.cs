namespace LuisEnmanuel_P1_P4.Models;

public record Autor
{
    public int IdAutor { get; init; }
    public string Nombres { get; init; } = "";
    public string Nacionalidad { get; init; } = "";
    public DateTime FechaNacimiento { get; init; }
    public double Sueldo { get; init; }
}
