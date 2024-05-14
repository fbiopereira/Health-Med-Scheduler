using AloDoutor.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AloDoutor.Infrastructure.Data.Configurations
{
    public class EspecialidadeMedicoConfiguration : IEntityTypeConfiguration<EspecialidadeMedico>
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
