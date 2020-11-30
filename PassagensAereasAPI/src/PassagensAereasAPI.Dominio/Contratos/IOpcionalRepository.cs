using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Contratos
{
    public interface IOpcionalRepository
    {
        Opcional SalvarOpcional(Opcional opcional);
        Opcional Obter(int id);
        Opcional AtualizarOpcional(int id, Opcional opcional);
        Opcional DeletarOpcional(int id);
        List<Opcional> ListarOpcionals();
    }
}