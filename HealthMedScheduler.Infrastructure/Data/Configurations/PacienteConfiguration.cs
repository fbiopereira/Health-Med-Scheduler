using HealthMedScheduler.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMedScheduler.Infrastructure.Data.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
              .IsRequired()
              .HasMaxLength(150)
              .HasColumnType("varchar")
               .HasColumnName("nome");

            builder.Property(c => c.Cpf)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("varchar")
            .HasColumnName("cpf");

            builder.Property(c => c.Cep)
             .IsRequired()
             .HasMaxLength(8)
             .HasColumnType("varchar")
             .HasColumnName("cep");

            builder.Property(c => c.Endereco)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnType("varchar")
             .HasColumnName("endereco");

            builder.Property(c => c.Telefone)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(11)
             .HasColumnName("telefone");

            builder.Property(c => c.Email)
              .IsRequired()
              .HasMaxLength(150)
              .HasColumnType("varchar")
               .HasColumnName("email");

            builder.Property(c => c.Estado)
              .IsRequired()
              .HasMaxLength(150)
              .HasColumnType("varchar")
               .HasColumnName("estado");

            builder.Property(c => c.Idade)
              .IsRequired()
              .HasMaxLength(3)
              .HasColumnType("varchar")
               .HasColumnName("idade");

            builder.HasMany(c => c.Agendamentos)
                .WithOne(c => c.Paciente)
                .HasForeignKey(c => c.PacienteId);

            builder.ToTable("paciente");
        }
    }
}
