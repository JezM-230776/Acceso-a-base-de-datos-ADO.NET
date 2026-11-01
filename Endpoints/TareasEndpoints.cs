using TareasAPI.Models;
using TareasAPI.Services;

namespace TareasAPI.Endpoints;

public static class TareasEndpoints
{
    // Método principal para registrar las rutas del recurso /tareas
    public static void Map(this WebApplication app) // el this es para que se pueda llamar desde afuera, es como un public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/tareas").WithTags("Tareas"); // el withtags es para que se agrupe en el swagger ui, para que se vea bonito y ordenado

        //GET /tareas
        group.MapGet("/", (ITareaService service) =>
            Results.Ok(service.ObtenerTodas())
        );

        //GET /tareas/{id}
        group.MapGet("/{id}", (int id, ITareaService service) =>
        {
            var tarea = service.ObtenerPorId(id);
            return tarea is null ? Results.NotFound(new { mensaje = "Tarea no encontrada" }) : Results.Ok(tarea);
        }
        );

        //POST /tareas
        group.MapPost("/", (Tarea nueva, ITareaService service) =>
        {
            var creada = service.Crear(nueva);
            return Results.Created($"/tareas/{creada.Id}", creada); //201
        }
        );
        //PUT /tareas/{id}
        group.MapPut("/{id}", (int id, Tarea actualizada, ITareaService service) =>
            service.Actualizar(id, actualizada) 
                ? Results.NoContent() //204
                : Results.NotFound(new { mensaje = "Tarea no encontrada" })
        );
        //DELETE /tareas/{id}
        group.MapDelete("/{id}", (int id, ITareaService service) =>
            service.Eliminar(id) 
                ? Results.NoContent() //204
                : Results.NotFound(new { mensaje = "Tarea no encontrada" })
        );
    }
}
