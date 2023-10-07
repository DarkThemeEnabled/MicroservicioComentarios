using Application.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComentarioCommand
    {
        public Task<Comentario> CreateComentario(Comentario comentario);
        public Task<Comentario> UpdateComentario(ComentarioRequest comentarioRequest, int comentarioId);
        public Task<Comentario> DeleteComentario(Comentario comentario);
    }
}
