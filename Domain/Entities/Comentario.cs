namespace Domain.Entities
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public Guid UsuarioId { get; set; }
        public int PromedioPuntajeId { get; set; }
        public Guid RecetaId { get; set; }
        public string Contenido { get; set; }
        public int PuntajeReceta { get; set; }
        public bool Modificado { get; set; }


    }
}
