using System.ComponentModel.DataAnnotations;

namespace LuisEnmanuel_P1_P4.Models;

public record Autor(
    [property: Key] int IdAutor,
    string Nombres,
    string Nacionalidad,
    DateTime FechaNacimiento,
    double Sueldo
    );



