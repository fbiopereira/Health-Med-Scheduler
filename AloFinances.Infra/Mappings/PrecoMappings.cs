using AloFinances.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AloFinances.Infra.Mappings
{
    public class PrecoMappings : IEntityTypeConfiguration<Preco>
    {        
        public void Configure(EntityTypeBuilder<Preco> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Valor)
               .HasColumnType("decimal(10, 2)")
               .IsRequired();         

            builder.ToTable("preco");
        }
    }
}
