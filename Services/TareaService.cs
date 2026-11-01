using TareasAPI.Models;

namespace TareasAPI.Services;

public class TareaService : ITareaService
{
    // Base de datos temporal en memoria
    private readonly List<Tarea> _tareas = new ()
    {
        new Tarea { Id = 1, Titulo = "Aprender .NET", Completada = false },
        new Tarea { Id = 2, Titulo = "Crear mi primera API", Completada = true }
    };

    public IEnumerable<Tarea> ObtenerTodas() =>_tareas; //forma limpia de poner return _tareas;
    
    public Tarea? ObtenerPorId(int id)
    {
        return _tareas.FirstOrDefault(t => t.Id == id);
    }

    public Tarea Crear(Tarea nueva)
    {
        nueva.Id = _tareas.Count > 0 ? _tareas.Max(t => t.Id) + 1 : 1;
        _tareas.Add(nueva);
        return nueva;
    }

    public bool Actualizar(int id, Tarea actualizada)
    {
        var tarea = ObtenerPorId(id);
        if (tarea is null)
            return false;

        tarea.Titulo = actualizada.Titulo;
        tarea.Descripcion = actualizada.Descripcion;
        tarea.Completada = actualizada.Completada;
        return true;
    }

    public bool Eliminar(int id)
    {
        var tarea = ObtenerPorId(id);
        if (tarea is null)
            return false;

        _tareas.Remove(tarea);
        return true;
    }
}
