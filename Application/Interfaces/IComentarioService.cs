using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IComentarioService
    {
        Task<ComentarioResponse> CreateComentario(ComentarioRequest request);
        Task<ComentarioResponse> UpdateComentario(ComentarioRequest request, int idComentario);
        Task<ComentarioResponse> DeleteComentario(int idComentario);
        Task<ComentarioResponse> GetComentarioById(int idComentario);
        Task<ComentarioResponse> GetComentarioByRecetaId(Guid RecetaId);
    }
}
