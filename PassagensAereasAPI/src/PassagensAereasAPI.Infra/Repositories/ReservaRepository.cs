using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private PassagensAereasContext contexto;

        public ReservaRepository(PassagensAereasContext contexto)
        {
            this.contexto = contexto;
        }

        public Reserva SalvarReserva(int idUsuario, Reserva reserva)
        {
            contexto.Usuarios.Include(u => u.Reservas).FirstOrDefault(r => r.Id == idUsuario).Reservas.Add(reserva);
            return reserva;
        }

        public Reserva Obter(int id)
        {
            return contexto.Reservas
            .Include(r => r.ReservaOpcional)
            .ThenInclude(ro => ro.Opcional)
            .Include(r => r.ClasseDeVoo)
            .Include(r => r.Trecho).ThenInclude(lo => lo.LocalOrigem)
            .Include(r => r.Trecho).ThenInclude(ld => ld.LocalDestino)
            .AsNoTracking().FirstOrDefault(r => r.Id == id);
        }

        public Reserva DeletarReserva(int id)
        {
            var reservaCadastrada = contexto.Reservas
            .Include(r => r.ReservaOpcional)
            .Include(r => r.ClasseDeVoo)
            .Include(r => r.Trecho)
            .FirstOrDefault(r => r.Id == id);

            if (reservaCadastrada != null)
                contexto.Reservas.Remove(reservaCadastrada);

            return reservaCadastrada;
        }

        public List<Reserva> ListarReservas(int idUsuario)
        {
            var usuario = contexto.Usuarios.Include(u => u.Reservas)
            .FirstOrDefault(a => a.Id == idUsuario);

            return contexto.Reservas
            .Include(r => r.ClasseDeVoo)
            .Include(r => r.Trecho).ThenInclude(lo => lo.LocalOrigem)
            .Include(r => r.Trecho).ThenInclude(ld => ld.LocalDestino)
            .Include(r => r.ReservaOpcional)
            .ThenInclude(ro => ro.Opcional)
            .Where(r => usuario.Reservas.Contains(r))
            .Select(o => o).ToList();
        }
    }
}