using Aplicacion.Pago;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PagoServicios.Controllers
{
    /// <summary>
    /// Pagos e Informes
    /// </summary>
    public class PagoController : ControllerGeneral
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public PagoController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Realizar Pago
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CuentaPagar> AddPago([FromBody]AddPago.AddPagoRequest body) {
            return await _mediator.Send(body);
        }



    }
}
