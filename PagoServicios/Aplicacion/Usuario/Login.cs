using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Aplicacion.Usuario
{
    public class Login
    {

        public class LoginRequest : IRequest <JsonObject>
        {
            public string? email { get; set; }
            public string? password { get; set; }
        }

        public class Manejador : IRequestHandler<LoginRequest, JsonObject>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<JsonObject> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                if(request.email == null  || request.email.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo email no puede estar vacio"); };
                if (request.password == null || request.password.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo password no puede estar vacio"); };
                if ((await _dbContext.Usuario.Where(c => c.email.Trim().ToUpper().Equals(request.email.Trim().ToUpper())).AsNoTracking().FirstOrDefaultAsync()) != null) 
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, "Usuario no existe");
                }
                else
                {
                    var rs = new JsonObject();
                    rs.Add("mensaje", "Ingreso correctamente");
                    return rs;
                }

            }
        }


    }
}
