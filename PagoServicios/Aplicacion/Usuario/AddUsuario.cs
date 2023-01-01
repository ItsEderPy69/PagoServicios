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
    public class AddUsuario
    {

        public class AddUsuarioRequest : IRequest <JsonObject>
        {
            public string? email { get; set; }
            public string? password { get; set; }
            public string? nombre { get; set; }
            public string? apelido { get; set; }            
            public int? numero_cedula { get; set; }
        }

        public class Manejador : IRequestHandler<AddUsuarioRequest, JsonObject>
        {
            private ApiDBContext _dbContext;
            public Manejador(ApiDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<JsonObject> Handle(AddUsuarioRequest request, CancellationToken cancellationToken)
            {
                if(request.email == null  || request.email.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo email no puede estar vacio"); };
                if (request.password == null || request.password.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo password no puede estar vacio"); };
                if (request.nombre == null || request.nombre.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo nombre no puede estar vacio"); };
                if (request.apelido == null || request.apelido.Trim().Equals("")) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo apelido no puede estar vacio"); };
                if (request.numero_cedula == null || request.numero_cedula == 0) { throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "El campo numero_cedula no puede estar vacio"); };
                if ((await _dbContext.Usuario.Where(c => c.email.Trim().ToUpper().Equals(request.email.Trim().ToUpper())).AsNoTracking().FirstOrDefaultAsync()) != null) 
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, "Usuario ya existe");
                }
                try{
                    var usuario = new Dominio.Usuario
                    {
                        email = request.email,  
                        password = request.password,
                        nombre = request.nombre,
                        apellido = request.apelido,                        
                        numero_cedula = (int)request.numero_cedula
                    };
                    _dbContext.Usuario.Add(usuario);
                    _dbContext.SaveChanges();
                    var rs = new JsonObject();
                    rs.Add("mensaje", "usuario creado correctamente");
                    return rs;
                }catch(Exception ex)
                {
                    throw new Exception("No se ha podido guardar el usuario\nDetalles:" + ex.Message);
                }

            }
        }


    }
}
