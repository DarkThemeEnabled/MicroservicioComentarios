using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.SComentario
{
    public class ComentarioService: IComentarioService
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
                //PromedioPuntajeId = request.PromedioPuntajeId,
                PuntajeReceta = request.PuntajeReceta,
                //RecetaId = request.RecetaId,
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
                    ComentarioId = comentarioToDelete.ComentarioId,
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

        public Task<ComentarioResponse> UpdateComentario(ComentarioRequest request, int idComentario)
        {
            throw new NotImplementedException();
        }

        private Task<ComentarioResponse> CreateComentarioResponse(Comentario uncomentario)
        {
            var comentario = new ComentarioResponse
            {
                ComentarioId = uncomentario.ComentarioId,
                Contenido = uncomentario.Contenido,
                PromedioPuntajeId = uncomentario.PromedioPuntajeId,
                PuntajeReceta = uncomentario.PuntajeReceta,
                UsuarioId = uncomentario.UsuarioId,
            };
            return Task.FromResult(comentario);
        }

        private async Task<bool> VerifyComentarioId(int comentarioId)
        {
            return (await _query.GetComentarioById(comentarioId) != null);
        }

    }
}
