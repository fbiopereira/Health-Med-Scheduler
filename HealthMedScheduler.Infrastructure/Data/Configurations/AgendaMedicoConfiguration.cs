using HealthMedScheduler.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HealthMedScheduler.Infrastructure.Data.Configurations
{
    public class AgendaMedicoConfiguration : IEntityTypeConfiguration<AgendaMedico>
    {
        public void Configure(EntityTypeBuilder<AgendaMedico> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.HoraInicio)
               .IsRequired()
               .HasColumnName("hora_inicio");

            builder.Property(c => c.HoraFim)
               .IsRequired()
               .HasColumnName("hora_fim");

            builder.Property(c => c.DiaSemana)
               .IsRequired();

            builder.HasOne(c => c.Medico)
               .WithMany(c => c.AgendasMedico);

            builder.ToTable("agendas_medico");
        }


    }
}
