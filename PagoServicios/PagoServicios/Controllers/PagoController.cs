using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PagoServicios.Controllers
{
    /// <summary>
    /// 
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


    }
}
