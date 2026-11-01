using MySql.Data.MySqlClient;
using TareasAPI.Models;

namespace TareasAPI.Services;

public class ProductoService : IProductoService
{
    private readonly string _connectionString;

    // Obtenemos la cadena de conexión desde appsettings.json
    public ProductoService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    // CONSULTAS (Obtener todos)
    public List<Producto> ObtenerTodos()
    {
        var productos = new List<Producto>();

        try
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Productos";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        productos.Add(new Producto
                        {
                            Id = Convert.ToInt32(lector["Id"]),
                            Nombre = lector["Nombre"].ToString() ?? string.Empty,
                            Descripcion = lector["Descripcion"]?.ToString(),
                            Precio = Convert.ToDecimal(lector["Precio"]),
                            Stock = Convert.ToInt32(lector["Stock"])
                        });
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error en BD al obtener productos: " + ex.Message);
        }

        return productos;
    }

    // BUSCAR (Obtener por Id)
    public Producto? ObtenerPorId(int id)
    {
        Producto? producto = null;

        try
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Productos WHERE Id = @Id";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            producto = new Producto
                            {
                                Id = Convert.ToInt32(lector["Id"]),
                                Nombre = lector["Nombre"].ToString() ?? string.Empty,
                                Descripcion = lector["Descripcion"]?.ToString(),
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Stock = Convert.ToInt32(lector["Stock"])
                            };
                        }
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error en BD al buscar producto: " + ex.Message);
        }

        return producto;
    }

    // INSERT (Insertar nuevo producto y devolver el Id generado)
    public int Insertar(Producto producto)
    {
        try
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = @"INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) 
                                 VALUES (@Nombre, @Descripcion, @Precio, @Stock);
                                 SELECT LAST_INSERT_ID();";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? string.Empty);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error en BD al insertar producto: " + ex.Message);
            return 0;
        }
    }

    // ACTUALIZAR
    public bool Actualizar(Producto producto)
    {
        try
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = @"UPDATE Productos 
                                 SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock 
                                 WHERE Id = @Id";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", producto.Id);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? string.Empty);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error en BD al actualizar producto: " + ex.Message);
            return false;
        }
    }

    // ELIMINAR
    public bool Eliminar(int id)
    {
        try
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                string query = "DELETE FROM Productos WHERE Id = @Id";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error en BD al eliminar producto: " + ex.Message);
            return false;
        }
    }
}