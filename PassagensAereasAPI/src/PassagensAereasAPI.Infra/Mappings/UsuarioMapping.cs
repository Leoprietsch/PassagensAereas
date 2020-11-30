using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Admin).IsRequired();
            builder.Property(p => p.PrimeiroNome).HasMaxLength(30).IsRequired();
            builder.Property(p => p.UltimoNome).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Cpf).HasMaxLength(11).IsRequired();
            builder.Property(p => p.DataDeNascimento).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Senha).HasMaxLength(150).IsRequired();
            builder.HasMany(p => p.Reservas).WithOne();
        }
    }
}