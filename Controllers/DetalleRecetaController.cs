using System;
using Microsoft.AspNetCore.Mvc;
using reportesApi.Services;
using reportesApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using reportesApi.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using reportesApi.Helpers;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using reportesApi.Models.Compras;
using ApiBackend.Models;

namespace reportesApi.Controllers
{
   
    [Route("api")]
    public class DetalleRecetaController: ControllerBase
    {
   
        private readonly DetalleRecetaService _DetalleRecetaService;
        private readonly ILogger<DetalleRecetaController> _logger;
  
        private readonly IJwtAuthenticationService _authService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        

        Encrypt enc = new Encrypt();

        public DetalleRecetaController(DetalleRecetaService DetalleRecetaService, ILogger<DetalleRecetaController> logger, IJwtAuthenticationService authService) {
            _DetalleRecetaService = DetalleRecetaService;
            _logger = logger;
       
            _authService = authService;
            // Configura la ruta base donde se almacenan los archivos.
            // Asegúrate de ajustar la ruta según tu estructura de directorios.

            
            
        }


        [HttpPost("InsertDetalleReceta")]
        public IActionResult InsertDetalleReceta([FromBody] DetalleRecetaModel entradas )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _DetalleRecetaService.InsertDetalleReceta(entradas);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpGet("GetDetalleReceta")]
        public IActionResult GetDetalleReceta()
        {
            var objectResponse = Helper.GetStructResponse();
            var resultado = _DetalleRecetaService.GetDetalleReceta();

            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";


                // Llamando a la función y recibiendo los dos valores.
                
                 objectResponse.response = resultado;
            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpPut("UpdateDetalleReceta")]
        public IActionResult UpdateDetalleReceta([FromBody] DetalleRecetaModel entradas )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = _DetalleRecetaService.UpdateDetalleReceta(entradas);

                ;

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }

        [HttpDelete("DeleteDetalleReceta/{id}")]
        public IActionResult DeleteDetalleReceta([FromRoute] int id )
        {
            var objectResponse = Helper.GetStructResponse();
            try
            {
                objectResponse.StatusCode = (int)HttpStatusCode.OK;
                objectResponse.success = true;
                objectResponse.message = "data cargado con exito";

                _DetalleRecetaService.DeleteDetalleReceta(id);

            }

            catch (System.Exception ex)
            {
                objectResponse.message = ex.Message;
            }

            return new JsonResult(objectResponse);
        }
    }
}