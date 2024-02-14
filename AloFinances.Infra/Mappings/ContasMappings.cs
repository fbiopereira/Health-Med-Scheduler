using AloFinances.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AloFinances.Infra.Mappings
{
    public class ContasMappings : IEntityTypeConfiguration<Contas>
    {        
        public void Configure(EntityTypeBuilder<Contas> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Valor)
               .HasColumnType("decimal(10, 2)")
               .IsRequired();

            builder.Property(c => c.DataCadastro)
               .IsRequired();

            builder.Property(c => c.DataVencimento)
               .IsRequired();

            builder.Property(c => c.StatusConta)
               .IsRequired();

            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.HasOne(c => c.Paciente)
                .WithMany(c => c.Contas);

            builder.HasOne(c => c.Medico)
                .WithMany(c => c.Contas);

            builder.ToTable("contas");
        }
    }
}
