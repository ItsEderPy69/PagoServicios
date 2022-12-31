using Aplicacion.Servicios;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PagoServicios.Controllers
{
    /// <summary>
    /// Cuentas a Pagar
    /// </summary>
    public class CuentaPagarController : ControllerGeneral
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public CuentaPagarController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// Lista de Cuentas Pendientes
        /// </summary>
        /// <param name="UsuarioID"></param>
        /// <returns></returns>
        [HttpGet("{UsuarioID}")]
        public async Task<List<CuentaPagar>> getPendientes([FromRoute] int? UsuarioID) {
            return await _mediator.Send(new GetPendientes.GetPendienteRequest {UsuarioID = UsuarioID });
        }

    }
}
