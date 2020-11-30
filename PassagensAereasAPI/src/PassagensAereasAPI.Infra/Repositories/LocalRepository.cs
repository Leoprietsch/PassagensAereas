using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private PassagensAereasContext contexto;

        public LocalRepository(PassagensAereasContext contexto)
        {
            this.contexto = contexto;
        }

        public Local SalvarLocal(Local local)
        {
            contexto.Locais.Add(local);
            return local;
        }

        public Local Obter(int id)
        {
            return contexto.Locais.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public Local AtualizarLocal(int id, Local local)
        {
            var localCadastrado = contexto.Locais.FirstOrDefault(a => a.Id == id);
            if (localCadastrado != null)
                localCadastrado.Atualizar(local);

            return localCadastrado;
        }

        public Local DeletarLocal(int id)
        {
            var localCadastrado = contexto.Locais.FirstOrDefault(a => a.Id == id);

            if (localCadastrado != null)
                contexto.Locais.Remove(localCadastrado);

            return localCadastrado;
        }

        public List<Local> ListarLocais()
        {
            return contexto.Locais.AsNoTracking().ToList();
        }

    }
}