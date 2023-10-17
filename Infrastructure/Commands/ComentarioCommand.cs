using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands
{
    public class ComentarioCommand : IComentarioCommand
    {
        private readonly MicroservicioComentarioContext _context;
        public ComentarioCommand(MicroservicioComentarioContext context)
        {
            _context = context;
        }

        public async Task<Comentario> CreateComentario(Comentario comentario)
        {
            try
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return comentario;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }

        public async Task<Comentario> DeleteComentario(Comentario comentario)
        {
            try
            {
                _context.Remove(comentario);
                await _context.SaveChangesAsync();
                return comentario;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }

        public async Task<Comentario> UpdateComentario(ComentarioRequest comentarioRequest, int comentarioId)
        {
            try
            {
                var comentarioToUpdate = await _context.Comentarios.FirstOrDefaultAsync(co => co.ComentarioId == comentarioId);
                comentarioToUpdate.UsuarioId = comentarioRequest.UsuarioId;
                comentarioToUpdate.PromedioPuntajeId = comentarioRequest.PromedioPuntajeId;
                comentarioToUpdate.RecetaId = comentarioRequest.RecetaId;
                comentarioToUpdate.Contenido = comentarioRequest.Contenido;
                comentarioToUpdate.PuntajeReceta = comentarioRequest.PuntajeReceta;
                comentarioToUpdate.Modificado = true;

                await _context.SaveChangesAsync();
                return comentarioToUpdate;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }
    }
}
