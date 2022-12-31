using Aplicacion.Servicios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagoServicios.Middleware;
using Persistencia;
using Swashbuckle.Swagger;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => 
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddMediatR(typeof(AddServicio.Manejador).Assembly);

//Se agrega contexto de la Base De Datos
builder.Services.AddDbContext<ApiDBContext>();

var app = builder.Build();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var context = new ApiDBContext())
{
    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
//app.UseSwaggerUI(c => {
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PagoServicios API");
//    c.RoutePrefix = string.Empty;         
//});


//Se agrega para manejo de excepciones
app.UseMiddleware<ManejadorErrorMiddleware>();
//Actualizar BD
app.Run();



