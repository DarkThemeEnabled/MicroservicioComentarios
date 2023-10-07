using Domain.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class MicroservicioComentarioContext: DbContext
    {
        public DbSet<Comentario> Comentarios { get; set; }

        public MicroservicioComentarioContext(DbContextOptions<MicroservicioComentarioContext> options):base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComentarioConfig());
        }

        
    }
}
