﻿namespace Application.Request
{
    public class ComentarioRequest
    {
        public Guid UsuarioId { get; set; }
        public int PromedioPuntajeId { get; set; }
        public Guid RecetaId { get; set; }
        public string Contenido { get; set; }
        public int PuntajeReceta { get; set; }

    }
}
