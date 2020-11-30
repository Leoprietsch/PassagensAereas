using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class ReservaMapping : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reserva");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Trecho).WithMany().IsRequired();
            builder.HasOne(p => p.ClasseDeVoo).WithMany().IsRequired();
            builder.HasMany(p => p.ReservaOpcional).WithOne();
        }
    }
}