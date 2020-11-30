using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Repositories
{
    public class ClasseDeVooRepository : IClasseDeVooRepository
    {
        private PassagensAereasContext contexto;

        public ClasseDeVooRepository(PassagensAereasContext contexto)
        {
            this.contexto = contexto;
        }


        public ClasseDeVoo SalvarClasseDeVoo(ClasseDeVoo classeDeVoo)
        {
            contexto.ClassesDeVoo.Add(classeDeVoo);
            return classeDeVoo;
        }
        public ClasseDeVoo Obter(int id)
        {
            return contexto.ClassesDeVoo.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
        public ClasseDeVoo AtualizarClasseDeVoo(int id, ClasseDeVoo classeDeVoo)
        {
            var classeDeVooCadastrada = contexto.ClassesDeVoo.FirstOrDefault(a => a.Id == id);
            if (classeDeVooCadastrada != null)
                classeDeVooCadastrada.Atualizar(classeDeVoo);

            return classeDeVooCadastrada;
        }
        public ClasseDeVoo DeletarClasseDeVoo(int id)
        {
            var classeDeVooCadastrada = contexto.ClassesDeVoo.FirstOrDefault(a => a.Id == id);

            if (classeDeVooCadastrada != null)
                contexto.ClassesDeVoo.Remove(classeDeVooCadastrada);

            return classeDeVooCadastrada;
        }
        public List<ClasseDeVoo> ListarClassesDeVoo()
        {
            return contexto.ClassesDeVoo.AsNoTracking().ToList();
        }

    }
}