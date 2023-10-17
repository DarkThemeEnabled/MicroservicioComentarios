using Application.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IComentarioCommand
    {
        public Task<Comentario> CreateComentario(Comentario comentario);
        public Task<Comentario> UpdateComentario(ComentarioRequest comentarioRequest, int comentarioId);
        public Task<Comentario> DeleteComentario(Comentario comentario);
    }
}
