

using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Contratos
{
    public interface IReservaRepository
    {
        Reserva SalvarReserva(int idUsuario, Reserva reserva);
        Reserva Obter(int id);
        Reserva DeletarReserva(int id);
        List<Reserva> ListarReservas(int idUsuario);

    }
}