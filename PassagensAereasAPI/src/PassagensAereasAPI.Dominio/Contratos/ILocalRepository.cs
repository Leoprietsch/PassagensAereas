using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Contratos
{
    public interface ILocalRepository
    {
        Local SalvarLocal(Local local);
        Local Obter(int id);
        Local AtualizarLocal(int id, Local local);
        Local DeletarLocal(int id);
        List<Local> ListarLocais();
    }
}