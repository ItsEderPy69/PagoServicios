using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PagoServicios.Controllers
{
    /// <summary>
    /// Controlador principal, todos heredan de este
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerGeneral : ControllerBase
    {
        /// <summary>
        /// Mediador entre Aplicacion y PagoServicios
        /// </summary>
        protected readonly IMediator _mediator;
        /// <summary>
        /// Se inyecta mediador
        /// </summary>
        /// <param name="mediator"></param>
        public ControllerGeneral(IMediator mediator)
        {
            this._mediator = mediator;
        }



        //protected IMediator _mediator => mediator ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
