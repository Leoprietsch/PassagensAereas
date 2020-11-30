using System.Collections.Generic;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Dominio.Contratos
{
    public interface IClasseDeVooRepository
    {
        ClasseDeVoo SalvarClasseDeVoo(ClasseDeVoo classeDeVoo);
        ClasseDeVoo Obter(int id);
        ClasseDeVoo AtualizarClasseDeVoo(int id, ClasseDeVoo classeDeVoo);
        ClasseDeVoo DeletarClasseDeVoo(int id);
        List<ClasseDeVoo> ListarClassesDeVoo();
    }
}