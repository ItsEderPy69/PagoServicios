using Aplicacion.Servicios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagoServicios.Middleware;
using Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(AddServicio.Manejador).Assembly);

//Se agrega contexto de la Base De Datos
builder.Services.AddDbContext<ApiDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var context = new ApiDBContext())
{
    context.Database.Migrate();
}

//Se agrega para manejo de excepciones
app.UseMiddleware<ManejadorErrorMiddleware>();
//Actualizar BD
app.Run();



