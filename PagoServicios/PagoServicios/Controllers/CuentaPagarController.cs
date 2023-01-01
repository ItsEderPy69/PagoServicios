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
        /// <param name="numero_cedula"></param>
        /// <returns></returns>
        [HttpGet("{numero_cedula}")]
        public async Task<List<CuentaPagar>> getPendientes([FromRoute] int? numero_cedula) {
            return await _mediator.Send(new GetPendientes.GetPendienteRequest {numero_cedula = numero_cedula });
        }

    }
}
