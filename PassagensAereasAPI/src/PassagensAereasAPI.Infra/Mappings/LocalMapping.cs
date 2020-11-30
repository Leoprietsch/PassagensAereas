using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class LocalMapping : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {
            builder.ToTable("Local");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Latitude).IsRequired();
            builder.Property(p => p.Longitude).IsRequired();
            builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();
        }
    }
}