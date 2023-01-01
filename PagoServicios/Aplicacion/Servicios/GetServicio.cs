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
    public class GetServicio
    {

        public class GetServicioRequest : IRequest <Dominio.Servicios>
        {
            public string? ID { get; set; }
        }

        public class Manejador : IRequestHandler<GetServicioRequest, Dominio.Servicios>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Dominio.Servicios> Handle(GetServicioRequest request, CancellationToken cancellationToken)
            {

                if (request.ID == null || request.ID.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "ID no puede estar vacío"); }                                
                return await _dbContext.Servicio.Where(c => c.ID.Equals(request.ID)).FirstAsync();
            }
        }


    }
}
