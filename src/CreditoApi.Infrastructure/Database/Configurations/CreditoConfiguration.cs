using CreditoApi.Domain.Creditos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditoApi.Infrastructure.Database.Configurations;

internal sealed class CreditoConfiguration : IEntityTypeConfiguration<Credito>
{
    public void Configure(EntityTypeBuilder<Credito> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.ToTable("credito");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();
        builder.Property(c => c.NumeroCredito).HasColumnName("numero_credito").IsRequired().HasMaxLength(50);
        builder.Property(c => c.NumeroNfse).HasColumnName("numero_nfse").IsRequired().HasMaxLength(50);
        builder.Property(c => c.DataConstituicao).HasColumnName("data_constituicao").IsRequired();
        builder.Property(c => c.ValorIssqn).HasColumnName("valor_issqn").IsRequired().HasColumnType("decimal(15,2)");
        builder.Property(c => c.TipoCredito).HasColumnName("tipo_credito").IsRequired().HasMaxLength(100);
        builder.Property(c => c.SimplesNacional).HasColumnName("simples_nacional").IsRequired();
        builder.Property(c => c.Aliquota).HasColumnName("aliquota").IsRequired().HasColumnType("decimal(5,2)");
        builder.Property(c => c.ValorFaturado).HasColumnName("valor_faturado").IsRequired().HasColumnType("decimal(15,2)");
        builder.Property(c => c.ValorDeducao).HasColumnName("valor_deducao").IsRequired().HasColumnType("decimal(15,2)");
        builder.Property(c => c.BaseCalculo).HasColumnName("base_calculo").IsRequired().HasColumnType("decimal(15,2)");

        builder.HasIndex(c => c.NumeroCredito).IsUnique();
    }
}
