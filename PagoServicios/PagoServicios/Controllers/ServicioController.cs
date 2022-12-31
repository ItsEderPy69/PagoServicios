using Aplicacion.Servicios;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PagoServicios.Controllers
{
    /// <summary>
    /// Controller de Servicios
    /// </summary>
    public class ServicioController : ControllerGeneral
    {
        /// <summary>
        /// Nueva instancia
        /// </summary>
        /// <param name="mediator"></param>
        public ServicioController(IMediator mediator) : base(mediator)
        {
        }
        /// <summary>
        /// Insertar Servicio
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<Servicios> Insertar([FromBody]AddServicio.AddServicioRequest body)
        {

            return await _mediator.Send(body);
        }
        /// <summary>
        /// Traer servicio
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("{ID}")]
        public async Task<Servicios> GetServicio([FromRoute] string ID)
        {
            return await _mediator.Send(new GetServicio.GetServicioRequest { ID = ID});
        }
        /// <summary>
        /// Busqueda de servicios
        /// </summary>
        /// <param name="SearchQuery">Parametro para busqueda Ej: "Tigo"</param>
        /// <returns></returns>
        [HttpGet("Search")]
        public async Task<List<Servicios>> SearcServicio(string? SearchQuery )
        {
            return await _mediator.Send(new SearchServicio.SearchServicioRequest { SearchQuery = (SearchQuery == null ? "" : SearchQuery.ToUpper())}); ;
        }
    }
}
