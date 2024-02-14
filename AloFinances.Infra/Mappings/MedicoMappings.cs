using AloFinances.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AloFinances.Infra.Mappings
{
    public class MedicoMappings : IEntityTypeConfiguration<Medico>
    {        
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar")
                .HasColumnName("nome");

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar")
                .HasColumnName("estado");

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

            builder.Property(c => c.Crm)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnType("varchar")
                .HasColumnName("crm");

            builder.Property(c => c.Endereco)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnType("varchar")
                .HasColumnName("endereco");

            builder.Property(c => c.Telefone)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnType("varchar")
                .HasColumnName("telefone");

            builder.Property(c => c.DataCriacao)
            .IsRequired()
            .HasColumnType("datetime")
                .HasColumnName("data_Criacao");

            builder.Property(c => c.Ativo)
            .IsRequired()
                .HasColumnName("ativo");

            builder.Property(c => c.Valor)
               .HasColumnType("decimal(10, 2)")
               .IsRequired();

            builder.HasMany(c => c.Contas)
                .WithOne(c => c.Medico)
                .HasForeignKey(c => c.MedicoId);

            builder.ToTable("medico");
        }
    }
}
