using AloDoutor.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AloDoutor.Infrastructure.Data.Configurations
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataHoraAtendimento)
               .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.StatusAgendamento)
               .IsRequired();

            builder.HasOne(c => c.EspecialidadeMedico)
               .WithMany(c => c.Agendamentos);

            builder.HasOne(c => c.Paciente)
               .WithMany(c => c.Agendamentos);

            builder.ToTable("agendamento");
        }
    }
}
