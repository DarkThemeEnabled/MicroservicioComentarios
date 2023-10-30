using Application.Request;
using Application.Response;

namespace Application.Interfaces
{
    public interface IComentarioService
    {
        Task<ComentarioResponse> CreateComentario(ComentarioRequest request);
        Task<UpdateComentarioResponse> UpdateComentario(UpdateComentarioRequest request, int idComentario);
        Task<ComentarioResponse> DeleteComentario(int idComentario);
        Task<ComentarioResponse> GetComentarioById(int idComentario);
        Task<List<ComentarioResponse>> GetComentarioByRecetaId(Guid RecetaId);
    }
}
