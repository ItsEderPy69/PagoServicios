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
    public class SearchServicio
    {

        public class SearchServicioRequest : IRequest <List<Dominio.Servicios>>
        {
            public string SearchQuery { get; set; } = "";
        }

        public class Manejador : IRequestHandler<SearchServicioRequest, List<Dominio.Servicios>>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<List<Dominio.Servicios>> Handle(SearchServicioRequest request, CancellationToken cancellationToken)
            {                
                return await _dbContext.Servicio.Where(c => EF.Functions.Like(c.Descripcion.ToUpper(), "%" + request.SearchQuery.Replace(" ","%") + "%")).AsNoTracking().ToListAsync();
            }
        }


    }
}
