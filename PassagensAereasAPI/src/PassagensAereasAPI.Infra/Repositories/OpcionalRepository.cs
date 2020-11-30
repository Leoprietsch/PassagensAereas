using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Repositories
{
    public class OpcionalRepository : IOpcionalRepository
    {
        private PassagensAereasContext contexto;

        public OpcionalRepository(PassagensAereasContext contexto)
        {
            this.contexto = contexto;
        }

        public Opcional SalvarOpcional(Opcional opcional)
        {
            contexto.Opcionais.Add(opcional);
            return opcional;
        }

        public Opcional Obter(int id)
        {
            return contexto.Opcionais.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public Opcional AtualizarOpcional(int id, Opcional opcional)
        {
            var opcionalCadastrado = contexto.Opcionais.FirstOrDefault(a => a.Id == id);
            if (opcionalCadastrado != null)
                opcionalCadastrado.Atualizar(opcional);

            return opcionalCadastrado;
        }

        public Opcional DeletarOpcional(int id)
        {
            var opcionalCadastrado = contexto.Opcionais.FirstOrDefault(a => a.Id == id);

            if (opcionalCadastrado != null)
                contexto.Opcionais.Remove(opcionalCadastrado);

            return opcionalCadastrado;
        }

        public List<Opcional> ListarOpcionals()
        {
            return contexto.Opcionais.AsNoTracking().ToList();
        }
    }
}