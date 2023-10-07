using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
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
                var result = await _comentarioService.CreateComentario(request);
                return new JsonResult(result) { StatusCode = 201 };
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
    }
}
