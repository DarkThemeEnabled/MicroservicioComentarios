using Application.Request;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComentarioService
    {
        Task<ComentarioResponse> CreateComentario(ComentarioRequest request);
        Task<ComentarioResponse> UpdateComentario(ComentarioRequest request, int idComentario);
        Task<ComentarioResponse> DeleteComentario(int idComentario);
        Task<ComentarioResponse> GetComentarioById(int idComentario);
    }
}
