﻿using Domain.Entities;

namespace Application.Interfaces
{
    public interface IComentarioQuery
    {
        Task<Comentario> GetComentarioById(int id);
        Task<Comentario> GetComentarioByRecetaId(Guid recetaId);
    }
}
