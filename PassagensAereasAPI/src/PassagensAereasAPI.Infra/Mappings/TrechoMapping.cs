using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class TrechoMapping : IEntityTypeConfiguration<Trecho>
    {
        public void Configure(EntityTypeBuilder<Trecho> builder)
        {
            builder.ToTable("Trecho");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.LocalOrigem).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.LocalDestino).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Distancia).IsRequired();
        }
    }
}