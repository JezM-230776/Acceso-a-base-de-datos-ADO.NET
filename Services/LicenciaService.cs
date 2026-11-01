using TareasAPI.Models;

namespace TareasAPI.Services;

public class LicenciaService : ILicenciaService
{
    // Información temporal de la licencia en memoria
    private readonly Licencia _licencia = new Licencia
    {
        Nombre = "Bob Esponja Pantalones Cuadrados",
        NumeroLicencia = "FQ-000112",
        FechaNacimiento = "14/07/1986",
        FechaVencimiento = "14/07/2030",
        TipoLicencia = "Clase B",
        Direccion = "Piña 124, Fondo de Bikini",
        Estado = true,
        Foto = "https://placehold.co/150x150/png"
    };

    public Licencia ObtenerInfo() => _licencia;
}
