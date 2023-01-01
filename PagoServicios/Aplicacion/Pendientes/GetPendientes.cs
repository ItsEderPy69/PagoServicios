using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class GetPendientes
    {
        public class GetPendienteRequest : IRequest <List<CuentaPagar>>
        {
            public int? numero_cedula { get; set; }
        }

        public class Manejador : IRequestHandler<GetPendienteRequest, List<CuentaPagar>>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<List<CuentaPagar>> Handle(GetPendienteRequest request, CancellationToken cancellationToken)
            {
                if (request.numero_cedula == null || request.numero_cedula==0) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "numero_cedula no puede estar vacío"); }
                //if ((await _dbContext.Usuario.Where(c => c.numero_cedula== request.numero_cedula).FirstOrDefaultAsync()) != null) { throw new ManejadorExcepcion(HttpStatusCode.NotFound, "Usuario no existe"); }                                
                int? userID = (await _dbContext.Usuario.Where(c => c.numero_cedula == request.numero_cedula).AsNoTracking().Select(c => c.ID).FirstOrDefaultAsync());
                if(userID == null) { throw new ManejadorExcepcion(HttpStatusCode.NotFound, "Usuario no existe"); }
                return await _dbContext.CuentaPagar
                    .Where(c => c.UsuarioID == userID && c.Saldo > 0)
                    .Include(c => c.Servicio)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }


    }
}
