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
        /// <returns></returns>S
        [HttpPost]
        public async Task<CuentaPagar> AddPago([FromBody]AddPago.AddPagoRequest body) {
            return await _mediator.Send(body);
        }
        /// <summary>
        /// Lista de Pagos por numero de cedula
        /// </summary>
        /// <param name="numero_cedula"></param>
        /// <param name="DesdeFecha"></param>
        /// <param name="HastaFecha"></param>
        /// <returns></returns>
        [HttpGet("{numero_cedula}")]
        public async Task<List<PagoRealizado>> GetPagos([FromRoute] int? numero_cedula, DateTime? DesdeFecha, DateTime? HastaFecha)
        {
            return await _mediator.Send(new GetPagos.GetPagosRequest
            {
                DesdeFecha = (DesdeFecha == null ? DateTime.Today : DesdeFecha),
                HastaFecha = (HastaFecha == null ? DateTime.Today : HastaFecha),
                numero_cedula = numero_cedula            
            });
        }



    }
}
