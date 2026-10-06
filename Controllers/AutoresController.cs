using LuisEnmanuel_P1_P4.Models;
using LuisEnmanuel_P1_P4.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuisEnmanuel_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController(IAutorService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAutores()
    {
        var autores = await service.ObtenerTodosAsync();
        return Ok(autores);
    }

   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAutor(int id)
    {
        var autor = await service.ObtenerPorIdAsync(id);

        if (autor is null)
        {
            return NotFound($"El autor con id {id} no existe.");
        }

        return Ok(autor);
    }


    [HttpPost]
    public async Task<IActionResult> PostAutor([FromBody] Autor autor)
    {
        var creado = await service.CrearAsync(autor);
        return CreatedAtAction(nameof(GetAutor), new { id = creado.IdAutor }, creado);
    }

   
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAutor(int id, [FromBody] Autor autor)
    {
        var actualizado = await service.ActualizarAsync(id, autor);

        if (!actualizado)
        {
            return NotFound($"El autor con id {id} no existe.");
        }

        return NoContent();
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAutor(int id)
    {
        var eliminado = await service.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound($"El autor con id {id} no existe.");
        }

        return NoContent();
    }
}
