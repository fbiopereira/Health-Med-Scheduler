using AloDoutor.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Infra.Data.Mappings
{
    public class EspecialidadeMedicoMappings : IEntityTypeConfiguration<EspecialidadeMedico>
    {
        public void Configure(EntityTypeBuilder<EspecialidadeMedico> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataRegistro)
             .IsRequired()
             .HasColumnType("datetime")
              .HasColumnName("dataRegistro");

            builder.HasOne(c => c.Medico)
               .WithMany(c => c.EspecialidadesMedicos);

            builder.HasOne(c => c.Especialidade)
               .WithMany(c => c.EspecialidadeMedicos);

            builder.HasMany(c => c.Agendamentos)
                .WithOne(c => c.EspecialidadeMedico)
                .HasForeignKey(c => c.EspecialidadeMedicoId);

            builder.ToTable("especialidadeMedico");
        }
    }
}
