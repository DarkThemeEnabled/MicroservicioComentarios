using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    public class ComentarioConfig : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentario");
            builder.HasKey(co => co.ComentarioId);
            builder.Property(co => co.ComentarioId)
                .ValueGeneratedOnAdd();
            builder.Property(co => co.Contenido)
                    .HasMaxLength(500)
                    .IsRequired();
        }
    }
}
