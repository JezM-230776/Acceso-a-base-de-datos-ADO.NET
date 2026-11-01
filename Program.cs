using TareasAPI.Endpoints;
using TareasAPI.Services;

var builder = WebApplication.CreateBuilder(args); //el builder es como la caja de herramientas donde se guardan los servicios y configuraciones de la app

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS es solo para los baackend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Nuestros servicios
builder.Services.AddSingleton<ITareaService, TareaService>();
builder.Services.AddSingleton<ILicenciaService, LicenciaService>();
builder.Services.AddSingleton<IProductoService, ProductoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    //swagger ui
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS
app.UseCors("AllowAll");

// En desarrollo se permite HTTP para que Expo Go pueda conectarse desde el celular.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Endpoints de tareas y licencia
TareasEndpoints.Map(app);
LicenciaEndpoints.Map(app);
ProductosEndpoints.Map(app);

app.Run();