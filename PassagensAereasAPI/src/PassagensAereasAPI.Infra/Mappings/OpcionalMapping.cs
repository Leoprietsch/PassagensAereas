using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class OpcionalMapping : IEntityTypeConfiguration<Opcional>
    {
        public void Configure(EntityTypeBuilder<Opcional> builder)
        {
            builder.ToTable("Opcional");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
        }
    }
}