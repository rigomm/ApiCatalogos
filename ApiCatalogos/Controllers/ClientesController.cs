using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClbDatCatalogos;
using ClbModCatalogos;
using ClbModCatalogos.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiCatalogos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        ClsDatCliente objDatCliente = null;
        private readonly AppSettings appSettings;
        public ClientesController(IOptions<AppSettings> AppSettings)
        {

            this.appSettings = AppSettings.Value;
        }

        [HttpGet("Buscar")]
        public async Task<IActionResult> Buscar(string textoBusqueda = "")
        {

            objDatCliente = new ClsDatCliente();
            try
            {
                var result = await objDatCliente.Buscar(this.appSettings.Conexion, textoBusqueda);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");

            }

        }

        [HttpGet("BuscarPaginacion")]
        public async Task<IActionResult> BuscarPaginacion(string textoBusqueda = "", int pagina = 1)
        {

            objDatCliente = new ClsDatCliente();
            try
            {
                var result = await objDatCliente.BuscarPaginacion(this.appSettings.Conexion,
                    textoBusqueda, pagina);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");

            }

        }

    }

}