using Aplicacion.Usuario;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace PagoServicios.Controllers
{
    /// <summary>
    /// Alta de Usuario, Login
    /// </summary>
    public class UsuarioController : ControllerGeneral
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public UsuarioController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// Insertar un usuario
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<JsonObject> Insertar([FromBody] AddUsuario.AddUsuarioRequest body)
        {            
            return await _mediator.Send(body);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<JsonObject> Login([FromBody] Login.LoginRequest body)
        {
            return await _mediator.Send(body);
        }

    }
}
