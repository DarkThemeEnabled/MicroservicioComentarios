﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comentario
    {
        public int ComentarioId {  get; set; }
        public int UsuarioId {  get; set; }
        public int PromedioPuntajeId { get; set; }
        public Guid RecetaId { get; set; }
        public string Contenido {  get; set; }
        public int PuntajeReceta {  get; set; }


    }
}