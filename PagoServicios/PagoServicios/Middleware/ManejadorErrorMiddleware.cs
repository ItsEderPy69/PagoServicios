using Aplicacion.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PagoServicios.Middleware
{
	/// <summary>
	/// Manejador de Excepciones
	/// </summary>
	public class ManejadorErrorMiddleware
	{
		private readonly ILogger<ManejadorErrorMiddleware> logger;
		private readonly RequestDelegate next;	

/// <summary>
/// Constructor
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
		public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger) 
		{
			this.next = next;
			this.logger = logger;
		}

/// <summary>
/// Llamada
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
		public async Task InvokeAsync(HttpContext context) 
		{
			try
			{
				await next(context);
			}
			catch (Exception ex) 
			{
				await ManejadorExcepcionAsync(context, ex, logger);
			}
		}

		private async Task ManejadorExcepcionAsync(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger) 
		{
			object errores = null;
			switch (ex) 
			{
				case ManejadorExcepcion me:
					logger.LogError(ex, "Manejador Error");
					errores = me.Errores;
					context.Response.StatusCode = (int)me.Codigo;
					break;
				case Exception e:
					logger.LogError(e, "Error de servidor");
					errores = string.IsNullOrWhiteSpace(e.Message + " " + e.InnerException) ? "Error" : e.Message + " " + e.InnerException;
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;

			}
			context.Response.ContentType = "application/json";
			if (errores != null) 
			{
				var resultados = JsonConvert.SerializeObject(new { errores });
				await context.Response.WriteAsync(resultados);
			}
		}

	}
}
