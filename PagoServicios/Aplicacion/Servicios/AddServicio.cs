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
    public class AddServicio
    {
        
        

        

        public class AddServicioRequest : IRequest <Dominio.Servicios>
        {
            public string? Descripcion { get; set; }
        }

        public class Manejador : IRequestHandler<AddServicioRequest, Dominio.Servicios>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Dominio.Servicios> Handle(AddServicioRequest request, CancellationToken cancellationToken)
            {

                if (request.Descripcion == null || request.Descripcion.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest,"Descripcion no puede estar vacío"); }                
                if ((await _dbContext.Servicio.Where(c => c.Descripcion.ToUpper().Trim().Equals(request.Descripcion.ToUpper().Trim())).FirstOrDefaultAsync()) != null) { throw new ManejadorExcepcion(HttpStatusCode.NotFound, "Servicio ya existe"); }
                _dbContext.Database.Migrate();
                Dominio.Servicios serv = new Dominio.Servicios { Descripcion = request.Descripcion };                
                _dbContext.Servicio.Add(serv);
                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex) {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, ex.Message);
                }
                
                return await _dbContext.Servicio.Where(c => c.Descripcion.Equals(serv.Descripcion)).FirstAsync();
            }
        }


    }
}
