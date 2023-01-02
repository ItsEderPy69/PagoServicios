using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;

namespace Aplicacion.Pago
{
    public class AddPago
    {
        public class AddPagoRequest : IRequest<CuentaPagar>
        {            
            public int? CuentaPagarID { get; set; }
            public decimal? Importe { get; set; }   
            public string? Observacion { get; set; }            
        }

        public class Manejador : IRequestHandler<AddPagoRequest, CuentaPagar>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<CuentaPagar> Handle(AddPagoRequest request, CancellationToken cancellationToken)
            {

                if (request.CuentaPagarID ==null||request.CuentaPagarID == 0) { throw new ManejadorExcepcion(HttpStatusCode.OK, "CuentaPagarID no puede estar vacío"); }
                var _CuentaPagar = await _dbContext.CuentaPagar.Where(c => c.ID == request.CuentaPagarID).AsNoTracking().FirstOrDefaultAsync();
                if (_CuentaPagar == null) { throw new ManejadorExcepcion(HttpStatusCode.OK, "Cuenta a pagar no existe"); }
                if (request.Importe == null||request.Importe <= 0) { throw new ManejadorExcepcion(HttpStatusCode.OK, "El importe debe ser mayor a 0"); }
                if (_CuentaPagar.Saldo < request.Importe) { throw new ManejadorExcepcion(HttpStatusCode.BadGateway, "El Saldo de la cuenta es menor al Importe ingresado" ); }

                var pago = new PagoRealizado {
                    CuentaPagarID = _CuentaPagar.ID,
                    Importe = (decimal)request.Importe,
                    FechaPago = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc),
                    Observacion = (request.Observacion == null ? "" : request.Observacion.Trim())
                };
                _CuentaPagar.Saldo -= pago.Importe;
                _dbContext.CuentaPagar.Update(_CuentaPagar);
                _dbContext.PagoRealizado.Add(pago);
                _dbContext.SaveChanges();
                return _CuentaPagar;
            }
        }


    }
}
