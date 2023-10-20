using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class ComentarioQuery : IComentarioQuery
    {
        private readonly MicroservicioComentarioContext _context;
        public ComentarioQuery(MicroservicioComentarioContext context)
        {
            _context = context;
        }

        public async Task<Comentario> GetComentarioById(int comentarioId)
        {
            try
            {
                var comentario = await _context.Comentarios.SingleOrDefaultAsync(co => co.ComentarioId == comentarioId);
                return comentario;
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un problema al buscar el comentario");
            }
        }
        public async Task<List<Comentario>> GetComentarioByRecetaId(Guid recetaId)
        {
            try
            {
                var comentario = await _context.Comentarios.Where(co => co.RecetaId == recetaId).ToListAsync();
                return comentario;
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un problema al buscar el comentario");
            }

        }
    }
}
