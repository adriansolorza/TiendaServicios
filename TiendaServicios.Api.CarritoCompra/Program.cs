using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteService;



var builder = WebApplication.CreateBuilder(args);

var servicesConfig = builder.Configuration.GetSection("Services");
var librosUrl = servicesConfig.GetValue<string>("Libros") ?? throw new InvalidOperationException("Missing configuration value for Services:Libros");
var autoresUrl = servicesConfig.GetValue<string>("Autores") ?? throw new InvalidOperationException("Missing configuration value for Services:Autores");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarritoContexto>(options =>
{
    var connection = builder.Configuration.GetConnectionString("ConexionDatabase");
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(librosUrl);
});

builder.Services.AddHttpClient("Autores", config =>
{
    config.BaseAddress = new Uri(autoresUrl);
});

builder.Services.AddScoped<ILibrosService, LibrosService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
