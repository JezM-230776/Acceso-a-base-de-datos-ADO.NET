using TareasAPI.Models;
using TareasAPI.Services;

namespace TareasAPI.Endpoints;

public static class ProductosEndpoints
{
    // Método principal para registrar las rutas del recurso /productos
    public static void Map(this WebApplication app)
    {
        var group = app.MapGroup("/productos").WithTags("Productos");

        //GET /productos
        group.MapGet("/", (IProductoService service) =>
            Results.Ok(service.ObtenerTodos())
        );

        //GET /productos/{id}
        group.MapGet("/{id}", (int id, IProductoService service) =>
        {
            var producto = service.ObtenerPorId(id);
            return producto is null ? Results.NotFound(new { mensaje = "Producto no encontrado" }) : Results.Ok(producto);
        }
        );

        //POST /productos
        group.MapPost("/", (Producto nuevo, IProductoService service) =>
        {
            var nuevoId = service.Insertar(nuevo);
            nuevo.Id = nuevoId;
            return Results.Created($"/productos/{nuevo.Id}", nuevo); //201
        }
        );

        //PUT /productos/{id}
        group.MapPut("/{id}", (int id, Producto actualizado, IProductoService service) =>
        {
            actualizado.Id = id;
            return service.Actualizar(actualizado)
                ? Results.NoContent() //204
                : Results.NotFound(new { mensaje = "Producto no encontrado" });
        }
        );

        //DELETE /productos/{id}
        group.MapDelete("/{id}", (int id, IProductoService service) =>
            service.Eliminar(id)
                ? Results.NoContent() //204
                : Results.NotFound(new { mensaje = "Producto no encontrado" })
        );
    }
}