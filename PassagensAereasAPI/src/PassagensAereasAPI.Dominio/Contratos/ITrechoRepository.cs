using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Contratos
{
    public interface ITrechoRepository
    {
        Trecho SalvarTrecho(Trecho trecho);
        Trecho Obter(int id);
        Trecho AtualizarTrecho(int id, Trecho trecho);
        Trecho DeletarTrecho(int id);
        List<Trecho> ListarTrechos();
    }
}