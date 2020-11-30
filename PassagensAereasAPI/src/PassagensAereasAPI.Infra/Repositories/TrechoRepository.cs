using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereasAPI.Dominio.Contratos;
using PassagensAereasAPI.Dominio.Entidades;

namespace PassagensAereasAPI.Infra.Repositories
{
    public class TrechoRepository : ITrechoRepository
    {
        private PassagensAereasContext contexto;

        public TrechoRepository(PassagensAereasContext contexto)
        {
            this.contexto = contexto;
        }

        public Trecho SalvarTrecho(Trecho trecho)
        {
            contexto.Trechos.Add(trecho);
            return trecho;
        }

        public Trecho Obter(int id)
        {
            return contexto.Trechos.Include(a => a.LocalOrigem).Include(a => a.LocalDestino).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public Trecho AtualizarTrecho(int id, Trecho trecho)
        {
            var classeDeVooCadastrada = contexto.Trechos.Include(a => a.LocalOrigem).Include(a => a.LocalDestino).FirstOrDefault(a => a.Id == id);
            if (classeDeVooCadastrada != null)
                classeDeVooCadastrada.Atualizar(trecho);

            return classeDeVooCadastrada;
        }

        public Trecho DeletarTrecho(int id)
        {
            var trechoCadastrado = contexto.Trechos.Include(a => a.LocalOrigem).Include(a => a.LocalDestino).FirstOrDefault(a => a.Id == id);

            if (trechoCadastrado != null)
                contexto.Trechos.Remove(trechoCadastrado);

            return trechoCadastrado;
        }

        public List<Trecho> ListarTrechos()
        {
            return contexto.Trechos.Include(a => a.LocalOrigem)
            .Include(a => a.LocalDestino)
            .AsNoTracking().ToList();
        }
    }
}