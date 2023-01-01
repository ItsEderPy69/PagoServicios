using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;

namespace Aplicacion.Pago
{
    public class GetPagos
    {
        public class GetPagosRequest : IRequest<List<PagoRealizado>>
        {            
            public DateTime? DesdeFecha { get; set; }
            public DateTime? HastaFecha { get; set; }
            //public string? ServicioID { get; set; }
            public int? numero_cedula { get; set; }
        }

        public class Manejador : IRequestHandler<GetPagosRequest, List<PagoRealizado>>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<List<PagoRealizado>> Handle(GetPagosRequest request, CancellationToken cancellationToken)
            {
                if (request.numero_cedula == null || request.numero_cedula == 0) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "numero_cedula no puede estar vacío"); }
                if (request.DesdeFecha == null) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "DesdeFecha no puede estar vacío"); }
                if (request.HastaFecha == null) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "HastaFecha no puede estar vacío"); }
                if (request.HastaFecha < request.DesdeFecha ) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "Ingrese un rango válido de fechasS"); }
                int? userID = (await _dbContext.Usuario.Where(c => c.numero_cedula == request.numero_cedula).AsNoTracking().Select(c => c.ID).FirstOrDefaultAsync());
                if (userID == null) { throw new ManejadorExcepcion(HttpStatusCode.NotFound, "Usuario no existe"); }
                var pagos = await _dbContext.PagoRealizado
                    .Include(p => p.CuentaPagar)
                    .Include(p => p.CuentaPagar.Servicio)
                    .Where(p => p.CuentaPagar.UsuarioID == userID && p.FechaPago >= DateTime.SpecifyKind((DateTime)request.DesdeFecha, DateTimeKind.Utc) && 
                    p.FechaPago <= DateTime.SpecifyKind((DateTime)request.HastaFecha, DateTimeKind.Utc))
                    .AsNoTracking()
                    .ToListAsync();
                if (pagos.Count == 0) { throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontraron pagos en esta fecha"); }
                return pagos;
            }


        }


    }
}
