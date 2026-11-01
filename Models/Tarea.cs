namespace TareasAPI.Models; //funciona como un apellido 
public class Tarea
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; } = string.Empty;
    public bool Completada { get; set; } = false;
}
