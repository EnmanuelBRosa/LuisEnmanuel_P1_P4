namespace LuisEnmanuel_P1_P4.Models;

public record Autor (

   long IdAutor,
   string Nombres,
   string Nacionalidad,
   DateTime FechaNacimiento,
   double Sueldo

)

{
    public Autor() : this(0, "", "", default, 0) { }
}
