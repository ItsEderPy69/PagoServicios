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
            public int? UsuarioID { get; set; }
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
                if (request.UsuarioID == null || request.UsuarioID==0) { throw new ManejadorExcepcion(HttpStatusCode.OK, "UsuarioID no puede estar vacío"); }                
                if ((await _dbContext.Usuario.Where(c => c.ID== request.UsuarioID).FirstOrDefaultAsync()) != null) { throw new ManejadorExcepcion(HttpStatusCode.OK, "Usuario no existe"); }                                
                return await _dbContext.CuentaPagar
                    .Where(c => c.UsuarioID == request.UsuarioID && c.Saldo > 0)
                    .Include(c => c.Servicio)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }


    }
}
