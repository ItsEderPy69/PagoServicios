using Aplicacion.Servicios;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagoServicios.Middleware;
using Persistencia;
using Swashbuckle.Swagger;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => 
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddMediatR(typeof(AddServicio.Manejador).Assembly);

PagoServicios.Configuration? config = JsonConvert.DeserializeObject<PagoServicios.Configuration>(File.ReadAllText("appsettings.json"));
//Se agrega contexto de la Base De Datos
builder.Services.AddDbContext<ApiDBContext>(db => { db.UseNpgsql(config.ConnectionStrings.DefaultConnection); });

var app = builder.Build();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var context = new ApiDBContext(config.ConnectionStrings.DefaultConnection))
{
    
    //se migran si existen cambios
    context.Database.Migrate();
    //se crean deudas automaticamente para poder probar minimo de 100mil y max de 1m
    foreach (var user in context.Usuario.AsNoTracking().ToList())
    {
        foreach (var servicio in context.Servicio.AsNoTracking().ToList())
        {
            if (context.CuentaPagar.Where(c => c.UsuarioID == user.ID && c.ServicioID == servicio.ID && c.Saldo > 0).AsNoTracking().FirstOrDefault() == null)
            {
                Random rnd = new Random();
                var Importe = rnd.Next(100000, 1000000);
                var cuenta = new CuentaPagar
                {
                    UsuarioID = user.ID,
                    Importe = Importe,
                    Saldo = Importe,
                    Concepto = "Deuda  " + DateTime.Now.ToShortDateString(),
                    cuota = 0,
                    ServicioID = servicio.ID
                };
                context.CuentaPagar.Add(cuenta);
            }
        }
    }
    context.SaveChanges();
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



