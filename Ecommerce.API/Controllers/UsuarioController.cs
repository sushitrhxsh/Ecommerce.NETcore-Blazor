using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Servicio.Contrato;
using Ecommerce.DTO;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Lista/{rol:alpha}/{buscar:alpha?}")]
        public async Task<IActionResult> Lista(string rol, string buscar = "NA")
        {
            var response = new ResponseDTO<List<UsuarioDTO>>();

            try
            {
                if(buscar == "NA") {
                    buscar = "";
                }

                response.EsCorrecto = true;
                response.Resultado = await _usuarioService.Lista(rol,buscar);

            } catch(Exception ex) {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

       [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = new ResponseDTO<UsuarioDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _usuarioService.Obtener(id);

            } catch(Exception ex) {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody]UsuarioDTO modelo)
        {
            var response = new ResponseDTO<UsuarioDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _usuarioService.Crear(modelo);

            } catch(Exception ex) {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost("Autorizacion")]
        public async Task<IActionResult> Autorizacion([FromBody]LoginDTO modelo)
        {
            var response = new ResponseDTO<SesionDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _usuarioService.Actorizacion(modelo);

            } catch(Exception ex) {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody]UsuarioDTO modelo)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _usuarioService.Editar(modelo);

            } catch(Exception ex) {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }
        
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar([FromBody]UsuarioDTO modelo)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _usuarioService.Eliminar(modelo);

            } catch(Exception ex) {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }

    }
}