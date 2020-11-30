using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class ReservaOpcionalMapping : IEntityTypeConfiguration<ReservaOpcional>
    {
        public void Configure(EntityTypeBuilder<ReservaOpcional> builder)
        {
            builder.ToTable("ReservaOpcional");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Opcional).WithMany().IsRequired();
        }
    }
}