using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCases.SComentario
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioCommand _command;
        private readonly IComentarioQuery _query;

        public ComentarioService(IComentarioCommand command, IComentarioQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<ComentarioResponse> CreateComentario(ComentarioRequest request)
        {
            var comentario = new Comentario
            {
                Contenido = request.Contenido,
                PromedioPuntajeId = request.PromedioPuntajeId,
                PuntajeReceta = request.PuntajeReceta,
                RecetaId = request.RecetaId,
                //UsuarioId = request.UsuarioId,

            };
            Comentario comentarioCreated = await _command.CreateComentario(comentario);
            return await CreateComentarioResponse(comentarioCreated);

        }

        public async Task<ComentarioResponse> DeleteComentario(int idComentario)
        {
            try
            {
                if (!await VerifyComentarioId(idComentario)) { throw new ExceptionNotFound("El id no existe"); }
                Comentario comentarioToDelete = await _command.DeleteComentario(await _query.GetComentarioById(idComentario));
                return new ComentarioResponse
                {
                    
                    Contenido = comentarioToDelete.Contenido,
                    PromedioPuntajeId = comentarioToDelete.PromedioPuntajeId,
                    PuntajeReceta = comentarioToDelete.PuntajeReceta,
                    UsuarioId = comentarioToDelete.UsuarioId,

                };
            }

            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error en la búsqueda de la receta: " + ex.Message); }
            catch (Conflict ex) { throw new Conflict("Error en la base de datos: " + ex.Message); }
            catch (ExceptionSintaxError) { throw new ExceptionSintaxError("Sintaxis incorrecta para el Id"); }
        }

        public async Task<ComentarioResponse> GetComentarioById(int idComentario)
        {
            try
            {
                var comentario = await _query.GetComentarioById(idComentario);
                if (comentario != null)
                {
                    return await CreateComentarioResponse(comentario);
                }
                else
                {
                    throw new ExceptionNotFound("No existe ningun comentario con ese ID");
                }
            }
            catch (ExceptionSintaxError)
            {
                throw new ExceptionSintaxError("Error en la sintaxis del id a buscar, pruebe ingresar el id con el formato válido");
            }
            catch (ExceptionNotFound e)
            {
                throw new ExceptionNotFound("Error en la búsqueda: " + e.Message);
            }
        }

        public async Task<List<ComentarioResponse>> GetComentarioByRecetaId(Guid RecetaId)
        {
            try
            {
                var comentario = await _query.GetComentarioByRecetaId(RecetaId);
                if (comentario != null)
                {
                    var comentarioResponse = new List <ComentarioResponse>();
                    foreach (var item in comentario)
                    {
                        comentarioResponse.Add(await CreateComentarioResponse(item));
                    }
                    return comentarioResponse;
                }
                else
                {
                    throw new ExceptionNotFound("No existe ningun comentario con ese ID");
                }
            }
            catch (ExceptionSintaxError)
            {
                throw new ExceptionSintaxError("Error en la sintaxis del id a buscar, pruebe ingresar el id con el formato válido");
            }
            catch (ExceptionNotFound e)
            {
                throw new ExceptionNotFound("Error en la búsqueda: " + e.Message);
            }
        }

        public async Task<ComentarioResponse> UpdateComentario(ComentarioRequest request, int idComentario)
        {
            try
            {
                var comentario = await _query.GetComentarioById(idComentario);
                if (comentario != null)
                {
                    comentario = await _command.UpdateComentario(request, idComentario);

                    return await CreateComentarioResponse(comentario);
                }
                else
                {
                    throw new ExceptionNotFound("No existe ningun comentario con ese ID");
                }


            }
            catch (Conflict ex)
            {
                throw new Conflict("Error en la implementación a la base de datos: " + ex.Message);
            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound("Error en la busqueda en la base de datos: " + ex.Message);
            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError("Error en la sintaxis de la mercadería a modificar: " + ex.Message);
            }
        }

        private Task<ComentarioResponse> CreateComentarioResponse(Comentario uncomentario)
        {
            var comentario = new ComentarioResponse
            {
                
                Contenido = uncomentario.Contenido,
                PromedioPuntajeId = uncomentario.PromedioPuntajeId,
                PuntajeReceta = uncomentario.PuntajeReceta,
                UsuarioId = uncomentario.UsuarioId,
                RecetaId = uncomentario.RecetaId,
            };
            return Task.FromResult(comentario);
        }

        private async Task<bool> VerifyComentarioId(int comentarioId)
        {
            return (await _query.GetComentarioById(comentarioId) != null);
        }

    }
}
