using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Entidades;
using PassagensAereasAPI.Infra.Mappings;

namespace PassagensAereasAPI.Infra
{
    public class PassagensAereasContext : DbContext
    {
        public PassagensAereasContext(DbContextOptions options) : base(options) { }

        public DbSet<ClasseDeVoo> ClassesDeVoo { get; set; }

        public DbSet<Local> Locais { get; set; }

        public DbSet<Opcional> Opcionais { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<ReservaOpcional> ReservaOpcional { get; set; }

        public DbSet<Trecho> Trechos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClasseDeVooMapping());
            modelBuilder.ApplyConfiguration(new LocalMapping());
            modelBuilder.ApplyConfiguration(new OpcionalMapping());
            modelBuilder.ApplyConfiguration(new ReservaMapping());
            modelBuilder.ApplyConfiguration(new ReservaOpcionalMapping());
            modelBuilder.ApplyConfiguration(new TrechoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }
    }
}