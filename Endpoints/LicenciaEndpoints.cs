using TareasAPI.Services;

namespace TareasAPI.Endpoints;

public static class LicenciaEndpoints
{
    // Registra la ruta GET /licencia
    public static void Map(this WebApplication app)
    {
        var group = app.MapGroup("/licencia").WithTags("Licencia");

        group.MapGet("/", (ILicenciaService service) =>
            Results.Ok(service.ObtenerInfo())
        );
    }
}
