using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Mappings
{
    public class ClasseDeVooMapping : IEntityTypeConfiguration<ClasseDeVoo>
    {
        public void Configure(EntityTypeBuilder<ClasseDeVoo> builder)
        {
            builder.ToTable("ClasseDeVoo");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao).HasMaxLength(30).IsRequired();
            builder.Property(p => p.ValorFixoDoVoo).IsRequired();
            builder.Property(p => p.ValorPorMilha).IsRequired();
        }
    }
}