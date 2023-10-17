using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioComentarios.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComentariosController : Controller
    {
        private readonly IComentarioService _comentarioService;

        public ComentariosController(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> CreateComentario(ComentarioRequest request)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = string.Format("https://localhost:7015/api/Receta/", request.RecetaId);
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await _comentarioService.CreateComentario(request);
                    return new JsonResult(result) { StatusCode = 201 };
                }
                else
                {
                    //Esto lo puse para que corra el codigo no se si deberia ir aca
                    return new JsonResult(new BadRequest { Message = "Hubo un error" }) { StatusCode = 400 };
                }
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
            }
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(ComentarioResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> UpdateComentario(int comentarioId, ComentarioRequest request)
        {
            try
            {
                var result = await _comentarioService.UpdateComentario(request, comentarioId);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
            }
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(ComentarioResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public async Task<IActionResult> DeleteComentario(int comentarioId)
        {
            try
            {
                var result = await _comentarioService.DeleteComentario(comentarioId);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
            }
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ComentarioResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public async Task<IActionResult> GetComentarioByid(int Id)
        {
            try
            {
                var result = await _comentarioService.GetComentarioById(Id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
        }

        [HttpGet("RecetaId/{Id}")]
        [ProducesResponseType(typeof(ComentarioResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public async Task<IActionResult> GetComentarioByRecetaId(Guid Id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = string.Format("https://localhost:7015/api/Receta/", Id);
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await _comentarioService.GetComentarioByRecetaId(Id);
                    return new JsonResult(result) { StatusCode = 201 };
                }
                else
                {
                    //Esto lo puse para que corra el codigo no se si deberia ir aca
                    return new JsonResult(new BadRequest { Message = "" }) { StatusCode = 400 };
                }
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
        }
    }
}
