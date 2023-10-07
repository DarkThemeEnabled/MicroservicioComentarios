using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class ComentarioQuery: IComentarioQuery
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
    }
}
